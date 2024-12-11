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
    public class CustomerPurchaseConfigurations : IEntityTypeConfiguration<CustomerPurchase>
    {
        public void Configure(EntityTypeBuilder<CustomerPurchase> builder)
        {
            builder.HasKey(cp => new { cp.PurchaseId, cp.CustomerId }); // PurchaseId and CustomerId as composite key

            builder.HasOne(cp => cp.Purchase)
                .WithMany(cp => cp.CustomerPurchases)
                 .HasForeignKey(cp => cp.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade);
            

            builder.HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerPurchases)
                .HasForeignKey(cp => cp.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Property(cp => cp.PurchaseQuantity).IsRequired();
            builder.Property(cp => cp.Status).IsRequired();
            builder.Property(cp => cp.PaymentMethod).IsRequired();
        }
    }
}
