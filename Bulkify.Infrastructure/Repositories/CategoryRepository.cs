using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Repository.Data;
using Microsoft.EntityFrameworkCore;
namespace Bulkify.Repository.Repositories
{
    public class CategoryRepository : GenericRepositories<Category>, ICategoryRepository
    {
        public CategoryRepository(BulkifyDbContext context) : base(context)
        {
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Set<Category>().AddAsync(category);

        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public Task<Customer?> GetCategoryByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        }
    }
}
