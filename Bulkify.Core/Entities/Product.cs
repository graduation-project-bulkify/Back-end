using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class Product: BaseEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string ImageSource { get; set; }
        public bool ApprovalStatus { get; set; }

        // Foreign Keys
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public  Supplier Supplier { get; set; }
        public  Category Category { get; set; }
        public ICollection<CustomerPurchase>? CustomerPurchases { get; set; }
        public ICollection<ProductRate>? ProductRates { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
    }
}
