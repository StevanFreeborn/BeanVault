namespace BeanVault.Services.AuthService.Infrastructure.Interfaces;

public interface IUserRepository
{
  Task<IdentityResult?> AddUserAsync(ApplicationUser user);
  Task<ApplicationUser?> GetUserByIdAsync(string id);
  Task<ApplicationUser?> GetUserByEmailAsync(string email);
  Task<ApplicationUser?> GetUserByUsernameAsync(string username);
}