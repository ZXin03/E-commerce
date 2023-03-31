using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Model
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string SellerUserName { get; set; }
        public string SellerPassword { get;set; }
        public int loginTry { get; set; }
        public string Status { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
