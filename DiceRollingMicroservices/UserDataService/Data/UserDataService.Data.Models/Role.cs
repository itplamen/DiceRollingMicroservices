namespace UserDataAccessService.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using UserDataAccessService.Data.Models.Contracts;

    public class Role : IdentityRole<int>, IEntity
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
