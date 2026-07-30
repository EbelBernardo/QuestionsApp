using Microsoft.JSInterop;
using Perguntas.Client.Models;

namespace Perguntas.Client.Services;

public class IndexedDbService
{
    private readonly IJSRuntime _jsRuntime;

    public IndexedDbService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }


    public async Task InitializeAsync()
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.open");
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _jsRuntime.InvokeAsync<List<Category>>(
            "database.getCategories");
    }

    public async Task<Category> GetCategoryAsync(Guid id)
    {
        return await _jsRuntime.InvokeAsync<Category>(
            "database.getCategoryById",
            id);
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.createCategory",
            category);
    }

    public async Task UpdateCategoryAsync(int Id, Category category)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.updateCategory",
            Id,
            category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.deleteCategory",
            id);
    }

    public async Task<List<Question>> GetQuestionsAsync(Guid categoryId)
    {
        return await _jsRuntime.InvokeAsync<List<Question>>(
            "database.getQuestions",
            categoryId);
    }

    public async Task<Question> GetQuestionAsync(Guid id)
    {
        return await _jsRuntime.InvokeAsync<Question>(
            "database.getQuestionById",
            id);
    }

    public async Task CreateQuestionAsync(Question category)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.createQuestion",
            category);
    }

    public async Task UpdateQuestionAsync(int Id, Question category)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.updateQuestion",
            Id,
            category);
    }

    public async Task DeleteQuestionAsync(Guid id)
    {
        await _jsRuntime.InvokeVoidAsync(
            "database.deleteQuestion",
            id);
    }
}