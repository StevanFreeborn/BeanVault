namespace BeanVault.Services.AuthService.Infrastructure.Data.Postgres;

public class PostgresDbContext : IdentityDbContext<ApplicationUser>
{
  public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
  {
  }

  public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}