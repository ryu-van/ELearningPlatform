using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Helpers;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long examId, bool status)
        {
            var existing = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examId);
            if (existing == null) throw new EntityNotFoundException("Exam", new[] { examId });
            existing.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Exam> CreateExamAsync(ExamRequest request)
        {
            var entity = new Exam
            {
                Title = request.Title,
                Description = request.Description,
                Duration = request.Duration,
                Instructions = request.Instructions,
                Type = request.Type,
                PassingScore = request.PassingScore,
                TotalMarks = request.TotalMarks,
                Status = request.Status,
                IsRandomized = request.IsRandomized,
                ShowResults = request.ShowResults,
                IsActive = request.IsActive,
                MaxAtttempts = request.MaxAtttempts,
                ParentExamId = request.ParentExamId
            };
            _context.Exams.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteExamAsync(long examId)
        {
            var existing = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examId);
            if (existing == null) throw new EntityNotFoundException("Exam", new[] { examId });
            _context.Exams.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Exam> GetExamByIdAsync(long examId)
        {
            var entity = await _context.Exams
                .FirstOrDefaultAsync(e => e.Id == examId);
            if (entity == null) throw new EntityNotFoundException("Exam", new[] { examId });
            return entity;
        }

        public async Task<PagedResponse<Exam>> GetPagedExamsAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Exams.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(e => (e.Title != null && e.Title.Contains(keyword)) || (e.Description != null && e.Description.Contains(keyword)));
            }
            if (isActive.HasValue)
            {
                query = query.Where(e => e.IsActive == isActive.Value);
            }
            query = query.OrderBy(e => e.Id);
            return await query.ToPagedResponseAsync(page, pageSize);
        }

        public async Task<Exam> UpdateExamAsync(long examId, ExamRequest request)
        {
            var existing = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examId);
            if (existing == null) throw new EntityNotFoundException("Exam", new[] { examId });
            existing.Title = request.Title;
            existing.Description = request.Description;
            existing.Duration = request.Duration;
            existing.Instructions = request.Instructions;
            existing.Type = request.Type;
            existing.PassingScore = request.PassingScore;
            existing.TotalMarks = request.TotalMarks;
            existing.Status = request.Status;
            existing.IsRandomized = request.IsRandomized;
            existing.ShowResults = request.ShowResults;
            existing.IsActive = request.IsActive;
            existing.MaxAtttempts = request.MaxAtttempts;
            existing.ParentExamId = request.ParentExamId;
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
