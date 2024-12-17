using Bulkify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Customer?> GetCategoryByEmailAsync(string email);
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        void DeleteCategory(Category category);




    }
}