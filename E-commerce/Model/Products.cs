using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Model
{
    public class Products
    {
        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Stock { get; set; }
        public int SellerId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }    
        public Seller Seller { get; set;}
    }
}
