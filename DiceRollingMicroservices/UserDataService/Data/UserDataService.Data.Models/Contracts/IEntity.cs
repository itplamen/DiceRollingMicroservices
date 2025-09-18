namespace UserDataAccessService.Data.Models.Contracts
{
    public interface IEntity
    {
        public int Id { get; set; }

        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        DateTime? DeletedOn { get; set; } 
    }
}
