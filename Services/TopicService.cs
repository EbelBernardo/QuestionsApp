using Questions.Models;
using static Supabase.Postgrest.Constants;

namespace Questions.Services
{
    public class TopicService
    {
        private readonly Supabase.Client _supabase;
        private readonly AuthService _authService;

        public TopicService(Supabase.Client supabase, AuthService authService)
        {
            _supabase = supabase;
            _authService = authService;
        }

        public async Task<List<Topic>> GetAllAsync(Guid subjectId)
        {
            return await _authService.ExecuteAsync(async () =>
            {
                var response = await _supabase
                    .From<Topic>()
                    .Where(t => t.SubjectID == subjectId)
                    .Order(t => t.Position, Ordering.Ascending)
                    .Order(t => t.Name, Ordering.Ascending)
                    .Get();

                return response.Models;

            }) ?? [];
        }

        public async Task<Topic?> GetAsync(Guid id)
        {
            return await _authService.ExecuteAsync(async () =>
            {
                var response = await _supabase
                    .From<Topic>()
                    .Where(t => t.ID == id)
                    .Single();

                return response;
            });
        }

        public async Task CreateAsync(Topic topic)
        {
            if(!_authService.IsAuthenticated)
                throw new InvalidOperationException("Usuário não autenticado.");

            topic.ID = Guid.NewGuid();
            topic.UserId = Guid.Parse(_authService.CurrentUserId!);

            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Topic>()
                    .Insert(topic);

                return true;
            });
        }

        public async Task UpdateAsync(Topic topic)
        {
            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Topic>()
                    .Where(t => t.ID == topic.ID)
                    .Update(topic);

                return true;
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Topic>()
                    .Where(t => t.ID == id)
                    .Delete();

                return true;
            });
        }
    }
}
