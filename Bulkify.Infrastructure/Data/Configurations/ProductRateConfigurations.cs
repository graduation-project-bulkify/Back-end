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
    public class ProductRateConfigurations : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.HasOne(pr => pr.Customer)
            .WithMany(c => c.ProductRates)
            .HasForeignKey(pr => pr.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.Product)
                .WithMany(p => p.ProductRates)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pr => pr.Timestamp)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(pr => new { pr.CustomerId, pr.ProductId }).IsUnique(); 
        }
    }
}
