namespace OperativeService.Api.Models
{
    using DiceRollingMicroservices.Common.Models.Request;

    public class ProfileRequest
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public IEnumerable<SortOptions> Sort { get; set; }

        public bool Desc { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10; 
    }
}
