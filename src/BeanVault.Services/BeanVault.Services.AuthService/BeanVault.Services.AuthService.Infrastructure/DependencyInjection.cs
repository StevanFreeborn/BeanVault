namespace BeanVault.Services.AuthService.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
  {
    services.Configure<JwtOptions>(
      config.GetSection(nameof(JwtOptions))
    );

    services.AddDbContext<PostgresDbContext>(
      options => options.UseNpgsql(
        config.GetConnectionString(nameof(PostgresDbContext)),
        options => options.MigrationsAssembly(typeof(PostgresDbContext).Assembly.FullName)
      ),
      ServiceLifetime.Transient
    );

    services.AddScoped<IUserRepository, PostgresUserRepository>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IJwtTokenService, JwtTokenService>();

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