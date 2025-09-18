namespace UserDataAccessService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
