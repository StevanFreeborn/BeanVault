namespace BeanVault.Services.AuthService.API.Dtos;

public class UserDto
{
  public string Id { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;

  public UserDto()
  {
  }

  public UserDto(ApplicationUser user)
  {
    Id = user.Id;
    Email = user.Email ?? string.Empty;
    Name = user.Name;
    PhoneNumber = user.PhoneNumber ?? string.Empty;
  }
}