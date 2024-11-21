namespace PROG_PART_2.Models
{
    // ViewModel used for processing payments in the system
    public class ProcessPaymentsViewModel
    {
        // List of claims that need to be processed for payments
        public List<Claim> Claims { get; set; }

        // Total number of claims that are being processed for payment
        public int TotalClaims { get; set; }

        // Total amount that needs to be paid for all the claims in the list
        public decimal TotalAmountToPay { get; set; }

        // Number of claims that have pending payments
        public int PendingPayments { get; set; }
    }
}
