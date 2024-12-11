using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class CustomerPurchase
    {
        public int PurchaseId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Purchase_quantity { get; set; }
        public string Statues { get; set; }
        public string Payment_method { get; set; }
        public Customer Customer { get; set; }
        public Purchase Purchase { get; set; }
        public Product Product { get; set; }
    }
}
