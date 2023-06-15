namespace BeanVault.Services.AuthService.Infrastructure.Interfaces;

public interface IJwtTokenService
{
  (DateTime tokenExpiration, string token) GenerateToken(ApplicationUser user, IList<string> roles);
}