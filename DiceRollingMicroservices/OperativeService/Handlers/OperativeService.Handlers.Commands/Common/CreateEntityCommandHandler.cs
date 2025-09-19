namespace OperativeService.Handlers.Commands.Common
{
    using AutoMapper;
    
    using MediatR;
    
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Response;

    public class CreateEntityCommandHandler<TCommand, TEntity> : IRequestHandler<TCommand, EntityResponse>
        where TCommand : IRequest<EntityResponse>
        where TEntity : BaseModel
    {
        private readonly IMapper mapper;
        private readonly IRepository<TEntity> repository;

        public CreateEntityCommandHandler(IMapper mapper, IRepository<TEntity> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<EntityResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            TEntity entity = mapper.Map<TEntity>(request);
            await repository.AddAsync(entity, cancellationToken);

            if (!string.IsNullOrEmpty(entity.Id))
            {
                return new EntityResponse() { Id = entity.Id };
            }

            return new EntityResponse("Could not create entity!");
        }
    }
}
