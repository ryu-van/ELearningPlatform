using E_learning_platform.Data;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseReview> AddReviewAsync(CourseReview review)
        {
            _context.CourseReviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(long id)
        {
            var review = await _context.CourseReviews.FindAsync(id);
            if (review == null) return false;

            _context.CourseReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetAverageRatingAsync(long courseId)
        {
            var ratings = await _context.CourseReviews
                .Where(r => r.CourseId == courseId && r.IsPublished)
                .Select(r => r.Rating)
                .ToListAsync();

            if (!ratings.Any()) return 0;
            return ratings.Average();
        }

        public async Task<CourseReview?> GetReviewByIdAsync(long id)
        {
            return await _context.CourseReviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PagedResponse<CourseReview>> GetReviewsByCourseIdAsync(long courseId, int page, int pageSize)
        {
             var query = _context.CourseReviews
                .Where(r => r.CourseId == courseId && r.IsPublished)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt);

            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<CourseReview>(items, totalItems, page, pageSize);
        }

        public async Task<bool> HasUserReviewedCourseAsync(long userId, long courseId)
        {
            return await _context.CourseReviews.AnyAsync(r => r.UserId == userId && r.CourseId == courseId);
        }
    }
}
