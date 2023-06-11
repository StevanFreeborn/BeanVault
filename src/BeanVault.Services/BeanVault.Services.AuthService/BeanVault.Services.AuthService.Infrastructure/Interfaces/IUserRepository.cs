namespace BeanVault.Services.AuthService.Infrastructure.Interfaces;

public interface IUserRepository
{
  Task<ApplicationUser> AddUserAsync(ApplicationUser user);
  Task<ApplicationUser?> GetUserByIdAsync(string id);
}