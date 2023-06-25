var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.Configure<ServiceUrls>(
  config.GetSection(nameof(ServiceUrls))
);

builder.Services.AddInfrastructure(config);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthHttpClientHandler>();
builder.Services.AddAuthHttpClient();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(
  config =>
  {
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
  }
);

builder.Services.AddSwagger();

builder.Services.AddCors(
  options => options.AddPolicy(
    "development",
    policy => policy
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000")
  )
);

builder.Services.AddJwtAuthentication(config);
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors("development");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();