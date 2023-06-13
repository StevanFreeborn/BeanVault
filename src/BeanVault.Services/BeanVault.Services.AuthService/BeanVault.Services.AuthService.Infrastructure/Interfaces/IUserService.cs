namespace BeanVault.Services.AuthService.Infrastructure.Interfaces;

public interface IUserService
{
  Task<ApplicationUser> AddUserAsync(ApplicationUser user);
  Task<ApplicationUser> LogInUserAsync(ApplicationUser user);
  Task<ApplicationUser> GetUserByIdAsync(string id);
  Task AddRoleToUserAsync(string userId, string roleName);
}