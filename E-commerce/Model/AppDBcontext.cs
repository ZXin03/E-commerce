using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Model
{
    public class AppDBcontext:DbContext
    {
        public DbSet<user> customers {get; set;}
        public DbSet<Address> addresses { get; set;}
        public DbSet<Category> categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Seller> sellers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=MSI\SQLEXPRESS;Database=E-Commerse;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
