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
    public class SupplierAddressConfigurations : IEntityTypeConfiguration<SupplierAddress>
    {
        public void Configure(EntityTypeBuilder<SupplierAddress> builder)
        {
            builder.HasKey(sa => sa.Id);
            builder.Property(sa => sa.City).IsRequired();
            builder.Property(sa => sa.Street).IsRequired();
            builder.Property(sa => sa.HomeNumber).IsRequired();
        }
    }
}
