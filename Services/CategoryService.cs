
using Perguntas.Client.Models;

namespace Perguntas.Client.Services
{
    public class CategoryService
    {
        private readonly Supabase.Client _supabase;

        public CategoryService(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var response = await _supabase
                .From<Category>()
                .Get();

            return response.Models;
        }

        public async Task<Category?> GetAsync(Guid id)
        {
            var response = await _supabase
                .From<Category>()
                .Where(c => c.ID == id)
                .Single();

            return response;
        }

        public async Task CreateAsync(Category category)
        {
            category.ID = Guid.NewGuid();

            await _supabase
                .From<Category>()
                .Insert(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _supabase
                .From<Category>()
                .Where(c => c.ID == category.ID)
                .Update(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _supabase
                .From<Category>()
                .Where(c => c.ID == id)
                .Delete();
        }
    }
}
