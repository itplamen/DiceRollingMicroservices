namespace UserDataAccessService.Api.Models
{
    public class RegisterRequest : AuthRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }
    }
}
