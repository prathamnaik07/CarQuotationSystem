
namespace CarQuotationSystem.models
{
    public class PurchaseRequest
    {
        public int PurchaseRequestId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        public bool WantsAccessories { get; set; }

        public decimal TotalAccessoryPrice { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public ICollection<PurchaseAccessory> PurchaseAccessories { get; set; }
            = new List<PurchaseAccessory>();
    }
}
