namespace OperativeService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GameRequest
    {
        [Required]
        public string GameId { get; set; }
    }
}
