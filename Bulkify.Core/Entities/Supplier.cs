using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class Supplier: BaseEntity
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public required string CommercialRegister { get; set; }
        public bool IsVerified { get; set; }
        public decimal SupplierRate { get; set; }

        // Navigation Properties
        public required ICollection<SupplierAddress> SupplierAddresses { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
