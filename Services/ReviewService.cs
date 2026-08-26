using Questions.Models;

namespace Questions.Services
{
    public class ReviewService
    {
        private readonly Supabase.Client _supabase;
        private readonly AuthService _authService;

        public ReviewService(Supabase.Client supabase, AuthService authService)
        {
            _supabase = supabase;
            _authService = authService;
        }

        public async Task<List<Review>> GetSinceAsync(DateTime fromUtc)
        {
            return await _authService.ExecuteAsync(async () =>
            {
                var response = await _supabase
                    .From<Review>()
                    .Where(r => r.AnsweredAt >= fromUtc)
                    .Get();

                return response.Models;

            }) ?? [];
        }

        public async Task CreateAsync(Review review)
        {
            if(!_authService.IsAuthenticated)
                throw new InvalidOperationException("Usuário não autenticado.");

            review.ID = Guid.NewGuid();
            review.ProfileID = Guid.Parse(_authService.CurrentUserId!);
            review.AnsweredAt = DateTime.UtcNow;

            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Review>()
                    .Insert(review);

                return true;
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await _authService.ExecuteAsync(async () =>
            {
                await _supabase
                    .From<Review>()
                    .Where(r=>r.ID == id)
                    .Delete();

                return true;
            });
        }
    }
}
