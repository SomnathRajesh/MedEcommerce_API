using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedEcommerce_DB
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=IN-LP-TR-19;Database=Med_Ecommerce;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        //}
        public DbSet<Medicine>? Medicines { get; set; }
        public virtual DbSet<Category>? Categories { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserRole>? Roles { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Order>? Orders { get; set;}
    }
}
