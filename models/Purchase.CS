namespace CarQuotationSystem.models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int PurchaseRequestId { get; set; }

        public int QuotationId { get; set; }

        public int UserId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal FinalAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}