namespace UserDataAccessService.Infrastructure.Mapping
{
    using AutoMapper;
    
    using Microsoft.AspNetCore.Identity;
    
    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Register;
    using UserDataAccessService.Handlers.Commands.Response;

    public class HandlersProfile : Profile
    {
        public HandlersProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<IdentityResult, BaseResponse>()
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded))
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.Description).ToList()));
        }
    }
}
