
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
  [UserPassword]
  public string Password { get; set; } = string.Empty;

  public ApplicationUser ToApplicationUser()
  {
    return new ApplicationUser
    {
      UserName = Email,
      Email = Email,
      NormalizedEmail = Email.ToLower(),
      Name = Name,
      PhoneNumber = PhoneNumber,
      Password = Password,
    };
  }
}