using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Helpers;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long questionId, bool status)
        {
            var existing = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (existing == null) throw new EntityNotFoundException("Question", new[] { questionId });
            existing.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Question> CreateQuestionAsync(QuestionRequest request)
        {
            var entity = new Question
            {
                Type = request.Type,
                Content = request.Content,
                Explanation = request.Explanation,
                Points = request.Points,
                OrderNo = request.OrderNo,
                DifficultyLevel = request.DifficultyLevel,
                IsActive = request.IsActive,
                SectionId = request.SectionId
            };
            _context.Questions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteQuestionAsync(long questionId)
        {
            var existing = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (existing == null) throw new EntityNotFoundException("Question", new[] { questionId });
            _context.Questions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Question> GetQuestionByIdAsync(long questionId)
        {
            var entity = await _context.Questions
                .Include(q => q.ExamSection)
                .FirstOrDefaultAsync(q => q.Id == questionId);
            if (entity == null) throw new EntityNotFoundException("Question", new[] { questionId });
            return entity;
        }

        public async Task<IEnumerable<Question>> GetQuestionsBySectionIdAsync(long sectionId)
        {
            return await _context.Questions
                .Where(q => q.SectionId == sectionId)
                .OrderBy(q => q.OrderNo)
                .ToListAsync();
        }

        public async Task<PagedResponse<Question>> GetPagedQuestionsAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Questions
                .Include(q => q.ExamSection)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(q => EF.Functions.Like(q.Content!, $"%{keyword}%"));
            }
            if (isActive.HasValue)
            {
                query = query.Where(q => q.IsActive == isActive.Value);
            }
            query = query.OrderBy(q => q.Id);
            return await query.ToPagedResponseAsync(page, pageSize);
        }

        public async Task<Question> UpdateQuestionAsync(long questionId, QuestionRequest request)
        {
            var existing = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (existing == null) throw new EntityNotFoundException("Question", new[] { questionId });
            existing.Type = request.Type;
            existing.Content = request.Content;
            existing.Explanation = request.Explanation;
            existing.Points = request.Points;
            existing.OrderNo = request.OrderNo;
            existing.DifficultyLevel = request.DifficultyLevel;
            existing.IsActive = request.IsActive;
            existing.SectionId = request.SectionId;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> IsOptionCorrectAsync(long questionId, long optionId)
        {
            var option = await _context.AnswerOptions
                .FirstOrDefaultAsync(o => o.Id == optionId && o.QuestionId == questionId);
            if (option == null) throw new EntityNotFoundException("AnswerOption", new[] { optionId });
            return option.IsCorrect;
        }
    }
}
