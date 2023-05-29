namespace BeanVault.Services.CouponService.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
  {
    ClassMapper.RegisterMappings();

    services.Configure<MongoDbOptions>(
      config.GetSection(key: nameof(MongoDbOptions))
    );

    services.AddSingleton<MongoDbContext>();

    return services;
  }
}