namespace BeanVault.Services.AuthService.Infrastructure.Interfaces;

public interface IJwtTokenService
{
  string GenerateToken(ApplicationUser user);
}