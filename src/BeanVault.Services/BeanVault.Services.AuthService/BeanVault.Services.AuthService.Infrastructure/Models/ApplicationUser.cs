namespace BeanVault.Services.AuthService.Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
  public string Name { get; set; } = string.Empty;

  // not stored in db
  // only used for carrying password from registration
  // action so can be then hashed and stored with user.
  [NotMapped]
  public string? Password { get; set; }
}