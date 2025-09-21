namespace OperativeService.Infrastructure.Mapping
{
    using AutoMapper;

    using DiceRollingMicroservices.MessageBus.Models;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Commands.Rounds;
    using OperativeService.Handlers.Commands.Users;

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

            CreateMap<UserMsg, CreateUserComman>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateUserComman, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));

            CreateMap<CreateRoundCommand, Round>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
                .ForMember(dest => dest.RoundNumber, opt => opt.MapFrom(src => src.RoundNumber))
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => new List<RollResult>() { src.RollResult }));
        }
    }
}
