namespace DiceRollingMicroservices.Common.Models.Response
{
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
