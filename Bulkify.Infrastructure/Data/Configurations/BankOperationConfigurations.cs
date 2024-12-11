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
    public class BankOperationConfigurations : IEntityTypeConfiguration<BankOperation>
    {
        public void Configure(EntityTypeBuilder<BankOperation> builder)
        {
            builder.HasKey(bo => bo.Id);
            builder.Property(bo => bo.BankAccount).IsRequired();
        }
    }
}
