using AutoMapper;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<EnrollmentResponse> EnrollUserAsync(long userId, long courseId)
        {
            if (await _enrollmentRepository.IsEnrolledAsync(userId, courseId))
            {
                throw new Exception("User is already enrolled in this course.");
            }

            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            if (!course.IsActive)
            {
                 throw new Exception("Course is not active.");
            }

            var enrollment = await _enrollmentRepository.CreateEnrollmentAsync(userId, courseId);
            
        
             var createdEnrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(enrollment.Id);

            return _mapper.Map<EnrollmentResponse>(createdEnrollment);
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetCourseEnrollmentsAsync(long courseId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<EnrollmentResponse>>(enrollments);
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetMyEnrollmentsAsync(long userId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<EnrollmentResponse>>(enrollments);
        }

        public async Task<PagedResponse<EnrollmentResponse>> GetPagedEnrollmentsAsync(string? keyword, string? status, int page, int pageSize)
        {
            var pagedEnrollments = await _enrollmentRepository.GetPagedEnrollmentsAsync(keyword, status, page, pageSize);
            var enrollmentResponses = _mapper.Map<IEnumerable<EnrollmentResponse>>(pagedEnrollments.Data);
            
            return new PagedResponse<EnrollmentResponse>(enrollmentResponses, pagedEnrollments.TotalItems, page, pageSize);
        }

        public async Task<bool> IsEnrolledAsync(long userId, long courseId)
        {
            return await _enrollmentRepository.IsEnrolledAsync(userId, courseId);
        }

        public async Task<bool> UnenrollUserAsync(long enrollmentId)
        {
            return await _enrollmentRepository.DeleteEnrollmentAsync(enrollmentId);
        }

        public async Task<bool> UpdateProgressAsync(long enrollmentId, decimal progress)
        {
            return await _enrollmentRepository.UpdateProgressAsync(enrollmentId, progress);
        }
    }
}
