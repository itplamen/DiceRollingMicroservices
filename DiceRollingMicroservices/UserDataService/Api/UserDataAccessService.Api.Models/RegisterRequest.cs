namespace UserDataAccessService.Api.Models
{
    using Microsoft.AspNetCore.Http;

    public class RegisterRequest : AuthRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IFormFile Image { get; set; }
    }
}
