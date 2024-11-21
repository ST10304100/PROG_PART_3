using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Models;

namespace PROG_PART_2.Controllers;

// Authorize access only for users with the "HR Manager" role
[Authorize(Roles = "HR Manager")]
public class ManageUsersController : Controller
{
    // Declare UserManager and RoleManager to interact with Identity system (manage users and roles)
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    // Constructor to initialize UserManager and RoleManager
    public ManageUsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: Index action to display a list of users and their basic details
    public async Task<IActionResult> Index()
    {
        // Retrieve all users and select relevant properties for the view model
        var users = await _userManager.Users
            .Select(user => new UserViewModel
            {
                Id = user.Id, // User ID
                Email = user.Email, // User email
                PhoneNumber = user.PhoneNumber // User phone number
            })
            .ToListAsync();

        // For each user, retrieve their roles and assign the role to the view model
        foreach (var user in users)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id); // Retrieve user by ID
            var roles = await _userManager.GetRolesAsync(appUser); // Get the user's roles
            user.Role = roles.FirstOrDefault(); // Assign the first role (if any)
        }

        // Return the list of users to the view
        return View(users);
    }

    // GET: Edit action to display user details for editing
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id); // Retrieve user by ID

        if (user == null)
            return NotFound(); // If user not found, return 404 Not Found

        // Retrieve user's roles and all available roles in the system
        var userRole = await _userManager.GetRolesAsync(user);
        var roles = _roleManager.Roles.Select(r => r.Name).ToList();

        // Create and populate the EditUserViewModel
        var model = new EditUserViewModel
        {
            Id = user.Id, // User ID
            Email = user.Email, // User email
            PhoneNumber = user.PhoneNumber, // User phone number
            Role = userRole.FirstOrDefault(), // Assign the user's current role
            Roles = roles // List of all available roles
        };

        // Return the model to the edit view
        return View(model);
    }

    // POST: Edit action to handle saving the edited user details
    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id); // Retrieve user by ID

        if (user == null)
            return NotFound(); // If user not found, return 404 Not Found

        // Get the user's current roles and remove them
        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Any())
            await _userManager.RemoveFromRoleAsync(user, userRoles.First()); // Remove existing role

        // Assign the new role to the user
        await _userManager.AddToRoleAsync(user, model.Role);

        // Update user's phone number with the new value
        user.PhoneNumber = model.PhoneNumber;

        // Save changes to the user in the Identity system
        await _userManager.UpdateAsync(user);

        // Redirect to the Index page after updating
        return RedirectToAction(nameof(Index));
    }

    // GET: Delete action to show the delete confirmation page for a user
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id); // Retrieve user by ID

        if (user == null)
            return NotFound(); // If user not found, return 404 Not Found

        // Create the model to confirm deletion and pass the user's email
        var model = new DeleteUserViewModel
        {
            Id = user.Id, // User ID
            Email = user.Email // User email
        };

        // Return the delete confirmation view with the model
        return View(model);
    }

    // POST: DeleteConfirmed action to delete the user after confirmation
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id); // Retrieve user by ID

        if (user == null)
            return NotFound(); // If user not found, return 404 Not Found

        // Delete the user from the Identity system
        await _userManager.DeleteAsync(user);

        // Redirect to the Index page after deletion
        return RedirectToAction(nameof(Index), "ManageUsers");
    }
}
