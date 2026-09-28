using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopM.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Review> Reviews { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
