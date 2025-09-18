namespace UserDataAccessService.Infrastructure.Mapping
{
    using AutoMapper;

    using UserDataAccessService.Api.Models;
    using UserDataAccessService.Handlers.Commands.Login;
    using UserDataAccessService.Handlers.Commands.Register;
    using UserDataAccessService.Handlers.Commands.Token;

    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<RegisterRequest, RegisterUserCommand>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<AuthRequest, LoginUserCommand>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<RefreshTokenRequest, CreateRefreshTokenCommand>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));

            CreateMap<RefreshTokenRequest, RevokeTokenCommand>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));
        }
    }
}
