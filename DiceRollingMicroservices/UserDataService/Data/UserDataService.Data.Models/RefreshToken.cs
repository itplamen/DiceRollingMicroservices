namespace UserDataAccessService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RefreshToken : BaseModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
