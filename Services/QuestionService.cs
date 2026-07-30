using Perguntas.Client.Models;

namespace Perguntas.Client.Services
{
    public class QuestionService
    {
        private readonly IndexedDbService _database;

        public QuestionService(IndexedDbService database)
        {
            _database = database;
        }

        public async Task<List<Question>> GetAllAsync(Guid categoryId)
        {
            return await _database.GetQuestionsAsync(categoryId);
        }

        public async Task<Question> GetAsync(Guid id)
        {
            return await _database.GetQuestionAsync(id);
        }

        public async Task CreateAsync(Question question)
        {
            question.ID = Guid.NewGuid();

            await _database.CreateQuestionAsync(question);
        }

        public async Task UpdateAsync(int id, Question question)
        {
            await _database.UpdateQuestionAsync(id, question);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _database.DeleteQuestionAsync(id);
        }
    }
}
