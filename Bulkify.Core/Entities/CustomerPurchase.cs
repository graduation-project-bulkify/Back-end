using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class CustomerPurchase: BaseEntity
    {
        public int PurchaseId { get; set; }
        public int CustomerId { get; set; }
        //public int ProductId { get; set; }
        public int PurchaseQuantity { get; set; }
        public required string Status { get; set; }
        public required string PaymentMethod { get; set; }
        public required Customer Customer { get; set; }
        public required Purchase Purchase { get; set; }
        //public required Product Product { get; set; }
    }
}
