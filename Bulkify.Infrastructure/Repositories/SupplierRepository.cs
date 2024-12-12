using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Repository.Repositories
{
    public class SupplierRepository : GenericRepositories<Supplier>, ISupplierRepository
    {
        public SupplierRepository(BulkifyDbContext context) : base(context)
        {
        }

        public async Task<Supplier> GetSupplierByEmailAsync(string email)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}
