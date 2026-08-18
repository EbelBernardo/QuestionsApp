
using Perguntas.Client.Models;

namespace Perguntas.Client.Services
{
    public class CategoryService
    {
        private readonly Supabase.Client _supabase;
        private readonly AuthService _authService;

        public CategoryService(Supabase.Client supabase, AuthService authService)
        {
            _supabase = supabase;
            _authService = authService;
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
            if (!_authService.IsAuthenticated)
                throw new InvalidOperationException("Usuário não autenticado.");

            category.ID = Guid.NewGuid();
            category.UserId = Guid.Parse(_authService.CurrentUserId!);

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
