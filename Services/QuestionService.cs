using Perguntas.Client.Models;

namespace Perguntas.Client.Services
{
    public class QuestionService
    {
        private readonly Supabase.Client _supabase;
        private readonly AuthService _authService;

        public QuestionService(Supabase.Client supabase, AuthService authService)
        {
            _supabase = supabase;
            _authService = authService;
        }

        public async Task<List<Question>> GetAllAsync(Guid categoryId)
        {
            var response = await _supabase
                .From<Question>()
                .Where(q => q.CategoryID == categoryId)
                .Get();

            return response.Models;
        }

        public async Task<Question?> GetAsync(Guid id)
        {
            var response = await _supabase
                .From<Question>()
                .Where(q => q.ID == id)
                .Single();

            return response;
        }

        public async Task CreateAsync(Question question)
        {
            if (!_authService.IsAuthenticated)
                throw new InvalidOperationException("Usuário não autenticado.");

            question.ID = Guid.NewGuid();
            question.UserId = Guid.Parse(_authService.CurrentUserId!);

            await _supabase
                .From<Question>()
                .Insert(question);
        }

        public async Task UpdateAsync(Question question)
        {
            await _supabase
                .From<Question>()
                .Where(q => q.ID == question.ID)
                .Update(question);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _supabase
                .From<Question>()
                .Where(q => q.ID == id)
                .Delete();
        }
    }
}
