namespace UserDataAccessService.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using UserDataAccessService.Data.Models.Contracts;

    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>(ReferenceEqualityComparer.Instance);
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        [Required] 
        public string Password { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
