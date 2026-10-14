

namespace CarQuotationSystem.models
{
    public class Quotation
    {
        public int QuotationId { get; set; }

        public int PurchaseRequestId { get; set; }

        public PurchaseRequest? PurchaseRequest { get; set; } 

        public int DealerId { get; set; }

        public Dealer? Dealer { get; set; } 

        public decimal CarPrice { get; set; }

        public decimal AccessoryPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal Insurance { get; set; }

        public decimal Tax { get; set; }

        public decimal FinalPrice { get; set; }

        public DateTime QuotationDate { get; set; }

        public DateTime ValidUntil { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}