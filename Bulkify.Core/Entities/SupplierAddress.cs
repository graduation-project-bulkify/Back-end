using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class SupplierAddress: BaseEntity
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }

        // Foreign Key
        public int SupplierId { get; set; }

        // Navigation Property
        public Supplier Supplier { get; set; }
    }
}
