namespace UserDataAccessService.Handlers.Commands.Contracts
{
    public interface IResponse
    {
        public bool IsSuccess { get; }

        IEnumerable<string> Errors { get; set; }
    }
}
