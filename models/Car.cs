namespace CarQuotationSystem.models
{
    public class Car
    {
      public int CarId {  get; set; }
        public string Brand {  get; set; } = string.Empty;
        public string Model {  get; set; } = string.Empty;
        public string Variant {  get; set; } = string.Empty;
        public decimal BasePrice {  get; set; }
    }
}
