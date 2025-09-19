namespace UserDataAccessService.Handlers.Commands.Response
{
    using DiceRollingMicroservices.Common.Models.Response;

    public class TokenResponse : BaseResponse
    {
        public TokenResponse() { }

        public TokenResponse(string errorMessage)
            : base(errorMessage) { }

        public string Token { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }
    }
}
