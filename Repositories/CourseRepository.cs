using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long courseId, bool status)
        {
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (existingCourse == null)
            {
                throw new EntityNotFoundException("Course", new[] { courseId });
            }

            existingCourse.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Course> CreateCourseAsync(CourseRequest request)
        {
            
            var course = new Course
            {
                Title = request.Title,
                Type = request.Type,
                Description = request.Description,
                Capacity = request.Capacity,
                ShortDescription = request.ShortDescription,
                ThumbnailUrl = request.ThumbnailUrl,
                Price = request.Price,
                Currency = request.Currency,
                Duration = request.Duration,
                Status = request.Status,
                IsActive = request.IsActive,
                isFeatured = request.isFeatured,
                EnrollmentStartDate = request.EnrollmentStartDate,
                EnrollmentEndDate = request.EnrollmentEndDate,
                CourseStartDate = request.CourseStartDate,
                CourseEndDate = request.CourseEndDate,
                TeacherId = request.TeacherId,
                LanguageId = request.LanguageId,
                LevelId = request.LevelId,
                BranchId = request.BranchId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteCourseAsync(long courseId)
        {
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (existingCourse == null)
            {
                throw new EntityNotFoundException("Course", new[] { courseId });
            }

            _context.Courses.Remove(existingCourse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Course> GetCourseByIdAsync(long courseId)
        {
            var existingCourse = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (existingCourse == null)
            {
                throw new EntityNotFoundException("Course", new[] { courseId });
            }

            return existingCourse;
        }

        public async Task<PagedResponse<Course>> GetPagedCoursesAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Branch)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{keyword}%") || EF.Functions.Like(c.Description, $"%{keyword}%"));
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

            return new PagedResponse<Course>(items, totalItems, page, pageSize);
        }

        public async Task<Course> UpdateCourseAsync(long courseId, CourseRequest request)
        {
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (existingCourse == null)
            {
                throw new EntityNotFoundException("Course", new[] { courseId });
            }

            existingCourse.Title = request.Title;
            existingCourse.Type = request.Type;
            existingCourse.Description = request.Description;
            existingCourse.Capacity = request.Capacity;
            existingCourse.ShortDescription = request.ShortDescription;
            existingCourse.ThumbnailUrl = request.ThumbnailUrl;
            existingCourse.Price = request.Price;
            existingCourse.Currency = request.Currency;
            existingCourse.Duration = request.Duration;
            existingCourse.Status = request.Status;
            existingCourse.IsActive = request.IsActive;
            existingCourse.isFeatured = request.isFeatured;
            existingCourse.EnrollmentStartDate = request.EnrollmentStartDate;
            existingCourse.EnrollmentEndDate = request.EnrollmentEndDate;
            existingCourse.CourseStartDate = request.CourseStartDate;
            existingCourse.CourseEndDate = request.CourseEndDate;
            existingCourse.TeacherId = request.TeacherId;
            existingCourse.LanguageId = request.LanguageId;
            existingCourse.LevelId = request.LevelId;
            existingCourse.BranchId = request.BranchId;

            await _context.SaveChangesAsync();
            return existingCourse;
        }
    }
}
