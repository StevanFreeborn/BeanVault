namespace BeanVault.Services.AuthService.API.Dtos;

public class LoginUserDto
{
  [Required]
  public string Username { get; set; } = string.Empty;

  [Required]
  public string Password { get; set; } = string.Empty;
  public ApplicationUser ToApplicationUser()
  {
    return new ApplicationUser
    {
      UserName = Username,
      Password = Password,
    };
  }
}