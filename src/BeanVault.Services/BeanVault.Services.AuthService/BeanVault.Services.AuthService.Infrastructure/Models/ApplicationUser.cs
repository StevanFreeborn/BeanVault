namespace BeanVault.Services.AuthService.Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
  public string Name { get; set; } = string.Empty;
}