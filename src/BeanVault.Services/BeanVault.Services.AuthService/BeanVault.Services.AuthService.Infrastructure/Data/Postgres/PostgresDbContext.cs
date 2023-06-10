namespace BeanVault.Services.AuthService.Infrastructure.Data.Postgres;

public class PostgresDbContext : IdentityDbContext<IdentityUser>
{
  public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}