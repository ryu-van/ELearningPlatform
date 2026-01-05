using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long lessonId, bool status)
        {
            var existingLesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (existingLesson == null)
            {
                throw new EntityNotFoundException("Lesson", new[] { lessonId });
            }

            existingLesson.isActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Lesson> CreateLessonAsync(LessonRequest request)
        {
            var lesson = new Lesson
            {
                Title = request.Title,
                Content = request.Content,
                Type = request.Type,
                DurationMinutes = request.DurationMinutes,
                OrderNo = request.OrderNo,
                isActive = request.isActive,
                ChapterId = request.ChapterId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<bool> DeleteLessonAsync(long lessonId)
        {
            var existingLesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (existingLesson == null)
            {
                throw new EntityNotFoundException("Lesson", new[] { lessonId });
            }

            _context.Lessons.Remove(existingLesson);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Lesson> GetLessonByIdAsync(long lessonId)
        {
            var existingLesson = await _context.Lessons
                .Include(l => l.Chapter)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (existingLesson == null)
            {
                throw new EntityNotFoundException("Lesson", new[] { lessonId });
            }

            return existingLesson;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByChapterIdAsync(long chapterId)
        {
            return await _context.Lessons
                .Where(l => l.ChapterId == chapterId)
                .OrderBy(l => l.OrderNo)
                .ToListAsync();
        }

        public async Task<PagedResponse<Lesson>> GetPagedLessonsAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Lessons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
             {
                 query = query.Where(l => (l.Title != null && l.Title.Contains(keyword)) || (l.Content != null && l.Content.Contains(keyword)));
             }

            if (isActive.HasValue)
            {
                query = query.Where(l => l.isActive == isActive.Value);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Lesson>(items, totalItems, page, pageSize);
        }

        public async Task<Lesson> UpdateLessonAsync(long lessonId, LessonRequest request)
        {
            var existingLesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (existingLesson == null)
            {
                throw new EntityNotFoundException("Lesson", new[] { lessonId });
            }

            existingLesson.Title = request.Title;
            existingLesson.Content = request.Content;
            existingLesson.Type = request.Type;
            existingLesson.DurationMinutes = request.DurationMinutes;
            existingLesson.OrderNo = request.OrderNo;
            existingLesson.isActive = request.isActive;
            existingLesson.ChapterId = request.ChapterId;

            await _context.SaveChangesAsync();
            return existingLesson;
        }
    }
}
