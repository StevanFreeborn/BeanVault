namespace BeanVault.Services.AuthService.API.Dtos;

public class AddRoleToUserDto
{
  [Required]
  public string UserId { get; set; } = string.Empty;

  [Required]
  public string RoleName { get; set; } = string.Empty;
}