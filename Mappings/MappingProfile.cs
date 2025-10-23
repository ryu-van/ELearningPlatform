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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(sc => sc.Feature.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(sc => sc.Feature.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(sc => sc.Feature.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(sc => sc.Feature.Description))
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
        }
    }
}
