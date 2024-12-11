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
    public class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(a => a.Email).IsUnique();
            builder.Property(a => a.Password).IsRequired();
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
        }
    }
}
