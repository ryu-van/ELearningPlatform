using E_learning_platform.Data;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment> CreateEnrollmentAsync(long userId, long courseId)
        {
            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                ErrolledAt = DateTime.UtcNow,
                Status = "Active",
                Progress = 0
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<bool> DeleteEnrollmentAsync(long id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(long id)
        {
            return await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(long courseId)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(long userId)
        {
            return await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<PagedResponse<Enrollment>> GetPagedEnrollmentsAsync(string? keyword, string? status, int page, int pageSize)
        {
            var query = _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(e => (e.User != null && e.User.FullName.Contains(keyword)) || 
                                         (e.Course != null && e.Course.Title != null && e.Course.Title.Contains(keyword)));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<Enrollment>(items, totalItems, page, pageSize);
        }

        public async Task<bool> IsEnrolledAsync(long userId, long courseId)
        {
            return await _context.Enrollments.AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task<bool> UpdateProgressAsync(long id, decimal progress)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return false;

            enrollment.Progress = progress;
            if (progress >= 100)
            {
                enrollment.Status = "Completed";
                enrollment.CompletedAt = DateTime.UtcNow;
            }
            
            enrollment.LastAccessedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
