namespace BeanVault.Services.AuthService.Infrastructure.Data.Postgres.Repositories;

public class PostgresUserRepository : IUserRepository
{
  private readonly PostgresDbContext _context;
  private readonly UserManager<ApplicationUser> _userManager;
  public PostgresUserRepository(PostgresDbContext context, UserManager<ApplicationUser> userManager)
  {
    _context = context;
    _userManager = userManager;
  }

  public async Task<IdentityResult?> AddUserAsync(ApplicationUser user)
  {
    return await _userManager.CreateAsync(user, user.Password!);
  }

  public async Task<ApplicationUser?> GetUserByIdAsync(string id)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
  }

  public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
  {
    return await _userManager.FindByEmailAsync(email);
  }

  public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
  {
    return await _context.Users.FirstOrDefaultAsync(
      u => u.UserName != null &&
      u.UserName.ToLower() == username.ToLower()
    );
  }
}