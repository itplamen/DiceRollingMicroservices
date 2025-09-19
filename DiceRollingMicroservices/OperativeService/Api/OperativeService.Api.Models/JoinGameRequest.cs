namespace OperativeService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class JoinGameRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string GameId { get; set; }
    }
}
