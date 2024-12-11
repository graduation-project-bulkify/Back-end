using Bulkify.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Repository.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.ImageSource).IsRequired();
            builder.Property(p => p.ApprovalStatus).IsRequired().HasDefaultValue(false);

            //builder.HasMany(p => p.CustomerPurchases)
            //    .WithOne(cp => cp.Product)
            //    .HasForeignKey(cp => cp.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(p => p.ProductRates)
            //    .WithOne(pr => pr.Product)
            //    .HasForeignKey(pr => pr.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p => p.Purchases)
            //   .WithOne(pu => pu.Product)
            //   .HasForeignKey(pu => pu.ProductId)
            //   .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
