using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class SupplierAddress: BaseEntity
    {
        public required string City { get; set; }
        public required string Street { get; set; }
        public int HomeNumber { get; set; }

        // Foreign Key
        public int SupplierId { get; set; }

        // Navigation Property
        public required Supplier Supplier { get; set; }
    }
}
