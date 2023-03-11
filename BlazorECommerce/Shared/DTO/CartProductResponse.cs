using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorECommerce.Shared.DTO
{
    public class CartProductResponse
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = "";
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; } = "";
        public string ImgUrl { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
