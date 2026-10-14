namespace CarQuotationSystem.dtos
{
    public class PurchaseRequestDto
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public bool WantsAccessories { get; set; }
        public List<int>? AccessoryIds { get; set; }
    }
}