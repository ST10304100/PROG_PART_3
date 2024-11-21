namespace PROG_PART_2.Models
{
    // ViewModel representing the data needed to delete a user
    public class DeleteUserViewModel
    {
        // The unique identifier of the user to be deleted
        public string Id { get; set; }

        // The email address of the user to be deleted
        public string Email { get; set; }
    }
}
