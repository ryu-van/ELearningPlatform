using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDbContext _context;

        public ChapterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long chapterId, bool status)
        {
            var existingChapter = await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == chapterId);

            if (existingChapter == null)
            {
                throw new EntityNotFoundException("Chapter", new[] { chapterId });
            }

            existingChapter.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Chapter> CreateChapterAsync(ChapterRequest request)
        {
            var chapter = new Chapter
            {
                Title = request.Title,
                OrderNo = request.OrderNo,
                Description = request.Description,
                IsActive = request.IsActive,
                CourseId = request.CourseId
            };

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
            return chapter;
        }

        public async Task<bool> DeleteChapterAsync(long chapterId)
        {
            var existingChapter = await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == chapterId);

            if (existingChapter == null)
            {
                throw new EntityNotFoundException("Chapter", new[] { chapterId });
            }

            _context.Chapters.Remove(existingChapter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Chapter> GetChapterByIdAsync(long chapterId)
        {
            var existingChapter = await _context.Chapters
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == chapterId);

            if (existingChapter == null)
            {
                throw new EntityNotFoundException("Chapter", new[] { chapterId });
            }

            return existingChapter;
        }

        public async Task<IEnumerable<Chapter>> GetChaptersByCourseIdAsync(long courseId)
        {
            return await _context.Chapters
                .Where(c => c.CourseId == courseId)
                .OrderBy(c => c.OrderNo)
                .ToListAsync();
        }

        public async Task<PagedResponse<Chapter>> GetPagedChaptersAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Chapters
                .Include(c => c.Course)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{keyword}%"));
            }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Chapter>(items, totalItems, page, pageSize);
        }

        public async Task<Chapter> UpdateChapterAsync(long chapterId, ChapterRequest request)
        {
            var existingChapter = await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == chapterId);

            if (existingChapter == null)
            {
                throw new EntityNotFoundException("Chapter", new[] { chapterId });
            }

            existingChapter.Title = request.Title;
            existingChapter.OrderNo = request.OrderNo;
            existingChapter.Description = request.Description;
            existingChapter.IsActive = request.IsActive;
            existingChapter.CourseId = request.CourseId;

            await _context.SaveChangesAsync();
            return existingChapter;
        }
    }
}
