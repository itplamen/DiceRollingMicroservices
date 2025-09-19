namespace OperativeService.Data.Models
{
    public class User : BaseModel
    {
        public int ExternalId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }
    }
}
