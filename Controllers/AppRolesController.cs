using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PROG_PART_2.Controllers
{
    // Controller for managing application roles
    public class AppRolesController : Controller
    {
        // Role manager to handle roles in ASP.NET Core Identity
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor that injects the RoleManager dependency
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Action method to display the list of roles
        public IActionResult Index()
        {
            // Fetch all roles from the RoleManager and pass them to the view
            var roles = _roleManager.Roles;
            return View(roles); // Returns the roles to be displayed in the Index view
        }

        // GET method for rendering the role creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Renders the Create view for adding a new role
        }

        // POST method for creating a new role
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            // Check if the role already exists in the system
            if (!await _roleManager.RoleExistsAsync(model.Name))
            {
                // If the role doesn't exist, create it using the RoleManager
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
            }
            // Redirect back to the Index action to display the updated list of roles
            return RedirectToAction("Index");
        }
    }
}
