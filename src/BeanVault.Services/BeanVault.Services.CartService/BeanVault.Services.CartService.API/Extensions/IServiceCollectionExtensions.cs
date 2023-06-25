namespace BeanVault.Services.CartService.API.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddAuthHttpClient(this IServiceCollection services)
  {
    services
    .AddHttpClient("authClient")
    .AddHttpMessageHandler<AuthHttpClientHandler>();

    return services;
  }

  public static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(
      options =>
      {
        var securityScheme = new OpenApiSecurityScheme
        {
          Name = "Authorization",
          Description = "Enter token in format: Bearer {token}",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme,
          }
        },
        Array.Empty<string>()
      },
        };

        options.AddSecurityDefinition(name: "Bearer", securityScheme: securityScheme);
        options.AddSecurityRequirement(securityRequirement);
        options.IncludeXmlComments(
          Path.Combine(
            AppContext.BaseDirectory,
            "BeanVault.Services.CartService.API.xml"
          ),
          true
        );
      }
    );

    return services;
  }

  public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, ConfigurationManager config)
  {
    var jwtOptions = new JwtOptions();
    config.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
    var jwtKey = Encoding.ASCII.GetBytes(jwtOptions.Secret);

    services
    .AddAuthentication(
      options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }
    )
    .AddJwtBearer(
      (options) =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
          ValidateIssuer = true,
          ValidIssuer = jwtOptions.Issuer,
          ValidateAudience = true,
          ValidAudience = jwtOptions.Audience,
        }
    );

    return services;
  }
}