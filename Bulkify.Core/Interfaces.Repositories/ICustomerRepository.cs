using Bulkify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Interfaces.Repositories
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
