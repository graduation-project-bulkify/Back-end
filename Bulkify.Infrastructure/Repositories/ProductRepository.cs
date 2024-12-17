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
    public class ProductRepository : GenericRepositories<Product>, IProductRepository
    {
        public ProductRepository(BulkifyDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
