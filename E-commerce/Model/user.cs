using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Model
{
    public class user
    {
        [Key]
        public int Customerid { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int LoginTry { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string? Status { get; set; }
        public string FullName { get; set; }   
        public string NRIC { get; set; }
        public int AddressId { get; set;}
        public Address? Address { get; set; }
    }
}
