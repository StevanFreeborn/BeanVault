var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddInfrastructure(config);

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

builder.Services.AddSwaggerGen();

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

var app = builder.Build();

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