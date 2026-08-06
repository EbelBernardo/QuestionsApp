
using Perguntas.Client.Models;

namespace Perguntas.Client.Services
{
    public class CategoryService
    {
        private readonly IndexedDbService _database;

        public CategoryService(IndexedDbService database)
        {
            _database = database;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _database.GetCategoriesAsync();
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await _database.GetCategoryAsync(id);
        }

        public async Task CreateAsync(Category category)
        {
            category.ID = Guid.NewGuid();

            await _database.CreateCategoryAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _database.UpdateCategoryAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _database.DeleteCategoryAsync(id);
        }
    }
}
