using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class Purchase: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        // Foreign Key
        public int ProductId { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}
