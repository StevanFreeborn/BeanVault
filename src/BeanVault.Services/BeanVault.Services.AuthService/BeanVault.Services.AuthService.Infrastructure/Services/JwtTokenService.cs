namespace BeanVault.Services.AuthService.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
  private readonly JwtOptions _options;
  public JwtTokenService(IOptions<JwtOptions> options)
  {
    _options = options.Value;
  }

  /// <summary>
  /// Generates a token with claims for given user.
  /// </summary>
  /// <param name="user"></param>
  /// <param name="roles"></param>
  /// <returns>The tokens expiration.</returns>
  /// <returns>The generated token.</returns>
  /// <exception cref="InvalidModelException"></exception>
  public (DateTime tokenExpiration, string token) GenerateToken(ApplicationUser user, IList<string> roles)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_options.Secret);

    if (user.Email is null)
    {
      throw new InvalidModelException("User does not have an email");
    }

    if (user.UserName is null)
    {
      throw new InvalidModelException("User does not have a username");
    }

    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim(JwtRegisteredClaimNames.Sub, user.Id),
      new Claim(JwtRegisteredClaimNames.Name, user.UserName)
    };

    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    var expiration = DateTime.UtcNow.AddDays(7);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Audience = _options.Audience,
      Issuer = _options.Issuer,
      Subject = new ClaimsIdentity(claims),
      Expires = expiration,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return (expiration, tokenHandler.WriteToken(token));
  }
}