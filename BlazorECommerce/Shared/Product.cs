
using System.ComponentModel.DataAnnotations.Schema;
namespace BlazorECommerce.Shared
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }
        public bool Featured { get; set; } = false;
        public int CategoryId { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
