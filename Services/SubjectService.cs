
using Questions.Models;

namespace Questions.Services
{
    public class SubjectService
    {
        private readonly Supabase.Client _supabase;
        private readonly AuthService _authService;

        public SubjectService(Supabase.Client supabase, AuthService authService)
        {
            _supabase = supabase;
            _authService = authService;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _authService.ExecuteAsync(async () =>
            {
                var response = await _supabase
                    .From<Subject>()
                    .Get();

                return response.Models;
            }) ?? [];
        }

        public async Task<Subject?> GetAsync(Guid id)
        {
            return await _authService.ExecuteAsync(async () =>
            {
                var response = await _supabase
                    .From<Subject>()
                    .Where(c => c.ID == id)
                    .Single();

                return response;
            });
        }

        public async Task CreateAsync(Subject subject)
        {
            if (!_authService.IsAuthenticated)
                throw new InvalidOperationException("Usuário não autenticado.");

            subject.ID = Guid.NewGuid();
            subject.UserId = Guid.Parse(_authService.CurrentUserId!);

            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Subject>()
                    .Insert(subject);

                return true;
            });
        }

        public async Task UpdateAsync(Subject subject)
        {
            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Subject>()
                    .Where(c => c.ID == subject.ID)
                    .Update(subject);

                return true;
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Subject>()
                    .Where(c => c.ID == id)
                    .Delete();

                return true;
            });
        }
    }
}
