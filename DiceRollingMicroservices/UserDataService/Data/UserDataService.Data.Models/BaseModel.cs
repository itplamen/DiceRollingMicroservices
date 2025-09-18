namespace UserDataAccessService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using UserDataAccessService.Data.Models.Contracts;

    public class BaseModel : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
