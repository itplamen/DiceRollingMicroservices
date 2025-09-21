namespace OperativeService.Handlers.Queries.Response
{
    using DiceRollingMicroservices.Common.Models.Response;

    public class ProfileResponse : BaseResponse
    {
        public ProfileResponse() { }

        public ProfileResponse(string errorMessage)
            : base(errorMessage) { }


        public UserResponse User { get; set; }

        public IEnumerable<GameResponse> Games { get; set; }
    }
}
