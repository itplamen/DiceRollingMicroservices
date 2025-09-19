namespace OperativeService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGameRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DieType { get; set; }

        [Required]
        public int MaxUsers { get; set; }

        [Required]
        public int MaxRounds { get; set; }

        [Required]
        public int DicePerUser { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
