using CarQuotationSystem.models;

namespace CarQuotationSystem.models
{
    public class PurchaseAccessory
    {
        public int PurchaseAccessoryId { get; set; }

        public int PurchaseRequestId { get; set; }

        public PurchaseRequest PurchaseRequest { get; set; } = null!;

        public int AccessoryId { get; set; }

        public Accessory Accessory { get; set; } = null!;
    }
}