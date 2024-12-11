using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Entities
{
    public class Customer: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public decimal XCoordinate { get; set; }
        public decimal YCoordinate { get; set; }

        public ICollection<CustomerPurchase> CustomerPurchases { get; set; }
        public ICollection<ProductRate> ProductRates { get; set; }
    }
}
