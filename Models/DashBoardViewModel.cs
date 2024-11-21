namespace PROG_PART_2.Models
{
    // ViewModel representing the data for the Dashboard
    public class DashboardViewModel
    {
        // The total number of claims submitted
        public int TotalClaims { get; set; }

        // The number of claims that are still pending approval
        public int PendingClaims { get; set; }

        // The number of claims that have been approved
        public int ApprovedClaims { get; set; }

        // The total amount of payments made (sum of completed payments)
        public decimal TotalPayments { get; set; }

        // The total amount of payments that are still pending
        public decimal PendingPayments { get; set; }

        // The total amount of payments that have been completed
        public decimal CompletedPayments { get; set; }
    }
}
