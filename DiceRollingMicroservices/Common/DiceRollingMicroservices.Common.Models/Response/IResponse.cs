namespace DiceRollingMicroservices.Common.Models.Response
{
    public interface IResponse
    {
        public bool IsSuccess { get; }

        IEnumerable<string> Errors { get; set; }
    }
}
