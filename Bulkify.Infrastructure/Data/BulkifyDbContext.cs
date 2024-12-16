using Bulkify.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Repository.Data
{
    public class BulkifyDbContext : DbContext
    {
        public BulkifyDbContext(DbContextOptions<BulkifyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerPurchase> CustomerPurchases { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BankOperation> BankAccounts { get; set; }


    }
}
