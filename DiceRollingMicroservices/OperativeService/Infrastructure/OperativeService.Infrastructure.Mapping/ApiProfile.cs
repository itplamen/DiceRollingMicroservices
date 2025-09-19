namespace OperativeService.Infrastructure.Mapping
{
    using AutoMapper;
    
    using OperativeService.Api.Models;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;

    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<CreateGameRequest, CreateGameCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DicePerUser, opt => opt.MapFrom(src => src.DicePerUser))
                .ForMember(dest => dest.MaxRounds, opt => opt.MapFrom(src => src.MaxRounds))
                .ForMember(dest => dest.MaxUsers, opt => opt.MapFrom(src => src.MaxUsers))
                .ForMember(dest => dest.DieType, opt => opt.MapFrom(src => Enum.Parse<DieType>(src.DieType)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<JoinGameRequest, GameCommand>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
