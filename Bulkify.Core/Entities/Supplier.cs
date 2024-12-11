using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class Supplier: BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string CommercialRegister { get; set; }
        public bool IsVerified { get; set; }
        public double SupplierRate { get; set; }

        // Navigation Properties
        public SupplierAddress SupplierAddress { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
