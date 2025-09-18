namespace UserDataAccessService.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using UserDataAccessService.Data.Models;

    public class UserDataServiceDbContext : IdentityDbContext<User, Role, int>
    {
        public UserDataServiceDbContext(DbContextOptions<UserDataServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
