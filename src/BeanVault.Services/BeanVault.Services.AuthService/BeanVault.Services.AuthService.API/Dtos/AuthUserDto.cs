namespace BeanVault.Services.AuthService.API.Dtos;

public class AuthUserDto
{
  public UserDto User { get; set; } = new UserDto();
  public string Token { get; set; } = string.Empty;

  public AuthUserDto()
  {
  }

  public AuthUserDto(ApplicationUser user)
  {
    User = new UserDto(user);
  }
}