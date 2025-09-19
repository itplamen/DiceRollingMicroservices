namespace OperativeService.Handlers.Commands.Response
{
    using DiceRollingMicroservices.Common.Models.Response;

    public class EntityResponse : BaseResponse
    {
        public EntityResponse() { }

        public EntityResponse(string errorMessage)
            : base(errorMessage) { }

        public string Id { get; set; }
    }
}
