using Bulkify.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Repository.Data.Configurations
{
    public class SupplierConfigurations : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FullName).IsRequired();
            builder.Property(s => s.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.Password).IsRequired();
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(s => s.CommercialRegister).IsRequired();
            builder.Property(s => s.IsVerified).IsRequired().HasDefaultValue(false);
            builder.Property(s => s.SupplierRate).HasColumnType("decimal(18, 2)").IsRequired().HasDefaultValue(0.00);


            builder.HasMany(s => s.SupplierAddresses)
                .WithOne(sa => sa.Supplier)
                .HasForeignKey(sa => sa.SupplierId)
                .OnDelete(DeleteBehavior.Cascade); // Addresses are deleted if the supplier is deleted

            builder.HasMany(s => s.Products)
                   .WithOne(p => p.Supplier)
                   .HasForeignKey(p => p.SupplierId);


        }
    }
}
