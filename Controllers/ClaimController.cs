using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG_PART_2.Data;
using PROG_PART_2.Models;

namespace PROG_PART_2.Controllers
{
    // This controller handles claim creation and viewing claim status
    [Authorize(Roles = "Lecturer")] // Restricts access to users with the "Lecturer" role
    public class ClaimController : Controller
    {
        private readonly ApplicationDBContext _context; // Database context for accessing the database
        private readonly UserManager<IdentityUser> _userManager; // Manages user-related operations
        private readonly IWebHostEnvironment _environment; // Provides access to web hosting environment settings

        // Constructor to inject dependencies
        public ClaimController(ApplicationDBContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: Renders the claim creation form
        public IActionResult Create()
        {
            return View(); // Returns the Create view
        }

        // POST: Handles form submission for creating a claim
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> Create(ClaimViewModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(model); // Return the same view with validation errors
            }

            // Ensure the date range is within one month and does not exceed 31 days
            if ((model.EndDate - model.StartDate).Days > 31 || model.StartDate.Month != model.EndDate.Month)
            {
                ModelState.AddModelError("", "The date range must be within one month and cannot exceed 31 days.");
                return View(model);
            }

            // Restrict claims to the current or previous month
            var currentDate = DateTime.Now;
            var validMonths = new[] { currentDate.Month, currentDate.AddMonths(-1).Month };
            if (!validMonths.Contains(model.StartDate.Month) || !validMonths.Contains(model.EndDate.Month))
            {
                ModelState.AddModelError("", "Claims can only be submitted for the current or previous month.");
                return View(model);
            }

            // Check if a claim for the same month already exists
            var user = await _userManager.GetUserAsync(User);
            bool existingClaim = _context.Claims.Any(c =>
                c.ApplicationUserId == user.Id &&
                c.StartDate.Month == model.StartDate.Month &&
                c.StartDate.Year == model.StartDate.Year &&
                (c.Status == "Pending" || c.Status == "Approved by Manager" || c.Status == "Approved by Coordinator")
            );
            if (existingClaim)
            {
                ModelState.AddModelError("", "You have already submitted a claim for this month, and it is either pending or approved.");
                ViewData["ClaimExists"] = existingClaim; // Pass existing claim status to the view
                return View(model);
            }

            // Validate that at least one supporting document is attached
            if (model.SupportingDocuments == null || model.SupportingDocuments.Count == 0)
            {
                ModelState.AddModelError("", "At least one supporting document must be attached.");
                return View(model);
            }

            // Validate each document
            foreach (var file in model.SupportingDocuments)
            {
                if (!IsValidDocument(file))
                {
                    ModelState.AddModelError("", "Only PDF files under 15 MB are allowed.");
                    return View(model);
                }
            }

            // Create a new claim object
            var claim = new Claim
            {
                HoursWorked = model.HoursWorked,
                HourlyRate = model.HourlyRate,
                Notes = model.Notes,
                DateSubmitted = DateTime.Now,
                ApplicationUserId = user.Id,
                TotalAmount = model.HourlyRate * model.HoursWorked,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            // Add the claim to the database
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            // Handle file uploads
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "documents");
            foreach (var file in model.SupportingDocuments)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); // Save the file
                }

                // Create a document record for each uploaded file
                var document = new Document
                {
                    ClaimId = claim.ClaimId,
                    DocumentName = uniqueFileName,
                    FilePath = filePath
                };

                _context.Documents.Add(document);
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Claim submitted successfully!"; // Display success message
            return RedirectToAction("Claims", "Lecturer"); // Redirect to the Lecturer's Claims page
        }

        // GET: View the status of claims for the logged-in user
        public async Task<IActionResult> ViewClaimStatus()
        {
            var currentUser = await _userManager.GetUserAsync(User); // Get the current user
            var claims = _context.Claims
                .Where(c => c.ApplicationUserId == currentUser.Id) // Filter claims by the current user
                .ToList();

            return View(claims); // Pass the claims to the view
        }

        // Validates uploaded document files
        public bool IsValidDocument(IFormFile file)
        {
            if (file == null)
            {
                return false; // File is not valid if null
            }

            // Check file type and size
            return file.ContentType == "application/pdf" && file.Length <= 15 * 1024 * 1024; // Max size: 15 MB
        }
    }
}
