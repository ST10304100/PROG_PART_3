using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    // ViewModel used for editing user details in the system
    public class EditUserViewModel
    {
        // Unique identifier for the user being edited
        public string Id { get; set; }

        // First name of the user
        public string FirstName { get; set; }

        // Last name of the user
        public string LastName { get; set; }

        // Phone number of the user, must match the South African format (+27 followed by 9 digits)
        [RegularExpression(@"^\+27\d{9}$", ErrorMessage = "Phone number must be in the format +27123456789")]
        public string PhoneNumber { get; set; }

        // Email address of the user
        public string Email { get; set; }

        // Role of the user in the system (e.g., Admin, HR, etc.)
        public string Role { get; set; }

        // List of available roles that can be assigned to the user
        public List<string> Roles { get; set; }
    }
}
