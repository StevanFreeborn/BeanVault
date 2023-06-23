namespace BeanVault.Services.CartService.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
  {
    ClassMapper.RegisterMappings();

    services.Configure<MongoDbOptions>(
      config.GetSection(nameof(MongoDbOptions))
    );

    services.AddSingleton<MongoDbContext>();
    services.AddScoped<ICartHeaderRepository, MongoCartHeaderRepository>();
    services.AddScoped<ICartDetailsRepository, MongoCartDetailsRepository>();

    return services;
  }
}