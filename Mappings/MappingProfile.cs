using AutoMapper;
using E_learning_platform.Models;
using E_learning_platform.DTOs.Responses;
using System.Linq;

namespace E_learning_platform.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RolePermission,FeatureResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(sc => sc.Feature != null ? sc.Feature.Id : 0))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(sc => sc.Feature != null ? sc.Feature.Code : string.Empty))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(sc => sc.Feature != null ? sc.Feature.Name : string.Empty))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(sc => sc.Feature != null ? sc.Feature.Description : string.Empty))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(sc => sc.IsEnabled));

            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(sc => sc.RolePermissions));

            CreateMap<Branch, BranchResponse>()
                .ForMember(debt => debt.Id, opt => opt.MapFrom(sc => sc.Id))
                .ForMember(debt => debt.Code, opt => opt.MapFrom(sc => sc.Code))
                .ForMember(debt => debt.Name, opt => opt.MapFrom(sc => sc.Name))
                .ForMember(debt => debt.Address, opt => opt.MapFrom(sc => sc.Address))
                .ForMember(debt => debt.City, opt => opt.MapFrom(sc => sc.City))
                .ForMember(debt => debt.Province, opt => opt.MapFrom(sc => sc.Province))
                .ForMember(debt => debt.PhoneNumber, opt => opt.MapFrom(sc => sc.PhoneNumber))
                .ForMember(debt => debt.IsActive, opt => opt.MapFrom(sc => sc.IsActive));

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty));

            CreateMap<Course, CourseResponse>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch));

            CreateMap<Chapter, ChapterResponse>();

            CreateMap<Lesson, LessonResponse>();
            
            CreateMap<Language, LanguageResponse>();
            CreateMap<Level, LevelResponse>();
            CreateMap<Video, VideoResponse>();
            CreateMap<Exam, ExamResponse>();
            CreateMap<Question, QuestionResponse>();

            CreateMap<Enrollment, EnrollmentResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : string.Empty))
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty));

            CreateMap<CourseReview, ReviewResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : string.Empty))
                .ForMember(dest => dest.UserAvatar, opt => opt.MapFrom(src => src.User != null ? src.User.AvatarUrl : string.Empty))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<ExamAttempt, ExamAttemptResponse>()
                .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam != null ? src.Exam.Title : string.Empty));

            CreateMap<UserAnswer, UserAnswerResponse>();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Register != null ? src.Register.FullName : string.Empty));
        }
    }
}
