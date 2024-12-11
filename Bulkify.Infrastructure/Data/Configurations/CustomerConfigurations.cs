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
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            // Primary Key (Assuming BaseEntity has Id)
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(c => c.Password)
                   .IsRequired();


            builder.Property(c => c.Gender)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(c => c.NationalId)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Street)
                .IsRequired()
               .HasMaxLength(255);

            builder.Property(c => c.HomeNumber)
                .IsRequired();

            builder.Property(c => c.XCoordinate)
               .HasColumnType("decimal(18, 12)")
               .IsRequired();


            builder.Property(c => c.YCoordinate)
                   .HasColumnType("decimal(18, 12)")
                   .IsRequired();


            // Relationships
            builder.HasMany(c => c.CustomerPurchases)
                .WithOne(cp => cp.Customer)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 


            builder.HasMany(c => c.ProductRates)
                .WithOne(pr => pr.Customer)
                .HasForeignKey(pr => pr.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);


            //Indexes
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => new { c.XCoordinate, c.YCoordinate });

        }
    }
}
