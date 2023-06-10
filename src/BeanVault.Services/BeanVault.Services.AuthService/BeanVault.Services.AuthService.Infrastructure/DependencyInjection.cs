using Microsoft.Extensions.Identity.Core;

namespace BeanVault.Services.AuthService.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
  {
    services.AddDbContext<PostgresDbContext>(
      options => options.UseNpgsql(
        config.GetConnectionString(nameof(PostgresDbContext)),
        options => options.MigrationsAssembly(typeof(PostgresDbContext).Assembly.FullName)
      ),
      ServiceLifetime.Transient
    );

    services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PostgresDbContext>()
    .AddDefaultTokenProviders();

    var db = services
    .BuildServiceProvider()
    .GetRequiredService<PostgresDbContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
      db.Database.Migrate();
    }

    return services;
  }
}