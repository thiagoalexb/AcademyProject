using Academy.Application.ViewModels;
using Academy.Domain.Entities;
using AutoMapper;

namespace Academy.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.LastUpdateDate, opt => opt.Ignore())
                .ForMember(x => x.LastUpdateDate, opt => opt.Ignore());

            CreateMap<User, UserUpdatePasswordViewModel>();
        }
    }
}
