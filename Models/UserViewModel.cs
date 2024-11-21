namespace PROG_PART_2.Models
{
    // ViewModel used to represent a user in the system
    public class UserViewModel
    {
        // Unique identifier for the user (typically the user ID)
        public string Id { get; set; }

        // The user's first name
        public string FirstName { get; set; }

        // The user's last name
        public string LastName { get; set; }

        // The user's phone number
        public string PhoneNumber { get; set; }

        // The user's email address
        public string Email { get; set; }

        // The role assigned to the user (e.g., HR Manager, Coordinator)
        public string Role { get; set; }
    }
}
