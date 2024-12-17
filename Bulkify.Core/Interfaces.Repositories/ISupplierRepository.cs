using Bulkify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Interfaces.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<Supplier> GetSupplierByEmailAsync(string email);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProduct(Product product);

    }
}
