using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorECommerce.Shared
{
    public class CardItem
    {
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
