namespace OperativeService.Handlers.Queries.Users
{
    using System.Linq.Expressions;

    using AutoMapper;
    
    using MediatR;
    
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Queries.Response;
    using OperativeService.Handlers.Queries.Users.FilterDecorators.Common;
    using OperativeService.Handlers.Queries.Users.SorterDecorators.Games;

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ProfileResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> usersRepository;
        private readonly IRepository<Game> gamesRepository;
        private readonly IRepository<Round> roundRepository;
        private readonly IEntityFilter<Game> entityFilter;
        private readonly IGameSorter sorter;

        public GetUserProfileQueryHandler(
            IMapper mapper, 
            IRepository<User> usersRepository, 
            IRepository<Game> gamesRepository, 
            IRepository<Round> roundRepository, 
            IEntityFilter<Game> entityFilter, 
            IGameSorter sorter)
        {
            this.mapper = mapper;
            this.usersRepository = usersRepository;
            this.gamesRepository = gamesRepository;
            this.roundRepository = roundRepository;
            this.entityFilter = entityFilter;
            this.sorter = sorter;
        }

        public async Task<ProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await usersRepository.FindAsync(x => x.ExternalId == request.ExternalId, cancellationToken);
            User user = users.FirstOrDefault();

            if (user != null) 
            {
                FilterQuery filterQuery = request;
                filterQuery.Id = user.Id;
                Expression<Func<Game, bool>> filter = entityFilter.Filter(filterQuery);

                IEnumerable<Game> games = await gamesRepository.FindAsync(filter, cancellationToken);
                IEnumerable<string> gameIds = games.Select(g => g.Id).ToList();

                IEnumerable<Round> rounds = await roundRepository.FindAsync(x => gameIds.Contains(x.GameId), cancellationToken);

                IEnumerable<string> otherUserIds = games.SelectMany(x => x.UserIds).Where(id => id != user.Id).Distinct();
                IEnumerable<User> otherUsers = await usersRepository.FindAsync(x => otherUserIds.Contains(x.Id), cancellationToken);

                IEnumerable<GameResponse> mappedGames = games.Select(game =>
                {
                    IEnumerable<Round> relatedRounds = rounds.Where(x => x.GameId == game.Id && x.Results.Any(y => y.UserId == user.Id)).Select(x =>
                    {
                        return new Round()
                        {
                            Id = x.Id,
                            RoundNumber = x.RoundNumber,
                            GameId = x.GameId,
                            Results = x.Results.Where(y => y.UserId == user.Id).ToList()
                        };
                    });
                    IEnumerable<User> relatedUsers = otherUsers.Where(x => game.UserIds.Contains(x.Id));

                    GameResponse gameResponse = mapper.Map<GameResponse>(game);
                    gameResponse.Users = mapper.Map<IEnumerable<UserResponse>>(relatedUsers);
                    gameResponse.Rounds = mapper.Map<IEnumerable<RoundResponse>>(relatedRounds);

                    return gameResponse;
                }).ToList();


                IEnumerable<GameResponse> sorted = sorter.Sort(mappedGames, request.Sort, request.Desc);

                return new ProfileResponse()
                {
                    User = mapper.Map<UserResponse>(user),
                    Games = sorted
                };
            }

            return new ProfileResponse("Cound not find user!");
        }
    }
}
