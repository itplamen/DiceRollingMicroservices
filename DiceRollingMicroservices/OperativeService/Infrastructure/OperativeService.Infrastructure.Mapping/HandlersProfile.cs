namespace OperativeService.Infrastructure.Mapping
{
    using AutoMapper;
 
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Queries.Games;

    public class HandlersProfile : Profile
    {
        public HandlersProfile()
        {
            CreateMap<CreateGameCommand, Game>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DieType, opt => opt.MapFrom(src => src.DieType))
                .ForMember(dest => dest.MaxUsers, opt => opt.MapFrom(src => src.MaxUsers))
                .ForMember(dest => dest.MaxRounds, opt => opt.MapFrom(src => src.MaxRounds))
                .ForMember(dest => dest.DicePerUser, opt => opt.MapFrom(src => src.DicePerUser))
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => new HashSet<string>() { src.UserId }));

            CreateMap<GameCommand, GetAvailableGamesQuery>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
