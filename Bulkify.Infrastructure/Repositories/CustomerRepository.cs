using Bulkify.Core.Entities;
using Bulkify.Service;

using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulkify.Core.Interfaces.Services;
namespace Bulkify.Repository.Repositories
{
    public class CustomerRepository : GenericRepositories<Customer>, ICustomerRepository
    {

        public CustomerRepository(BulkifyDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
