namespace UserDataAccessService.Handlers.Commands.Response
{
    using UserDataAccessService.Handlers.Commands.Contracts;

    public class BaseResponse : IResponse
    {
        public BaseResponse() { }

        public BaseResponse(string errorMessage)
        {
            Errors = new List<string>() { errorMessage };
        }

        public bool IsSuccess => Errors == null || !Errors.Any();

        public IEnumerable<string> Errors { get; set; }
    }
}
