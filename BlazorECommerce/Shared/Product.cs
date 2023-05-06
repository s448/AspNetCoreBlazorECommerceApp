
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlazorECommerce.Shared
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }
        public bool Featured { get; set; } = false;
        public int CategoryId { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
