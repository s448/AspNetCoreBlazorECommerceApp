namespace BlazorEcommerce.Shared
{
    public class OrderOverviewResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductTitle{ get; set; }
        public string ProductImageUrl { get; set; }
    }
}