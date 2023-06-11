namespace BeanVault.Services.AuthService.API.Dtos;

public class AddUserDto
{
  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;

  [Required]
  [MaxLength(150)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [Phone]
  public string PhoneNumber { get; set; } = string.Empty;

  [Required]
  [MinLength(8)]
  [MaxLength(150)]
  public string Password { get; set; } = string.Empty;
}