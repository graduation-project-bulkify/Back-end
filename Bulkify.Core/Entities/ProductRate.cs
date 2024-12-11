using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class ProductRate: BaseEntity
    {
        public int Rate { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign Keys
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public required Customer Customer { get; set; }
        public required Product Product { get; set; }
    }
}
