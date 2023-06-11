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

  public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
  {
    var result = await _userManager.CreateAsync(user, user.Password!);

    if (result.Succeeded == false)
    {
      throw new AggregateException(
        result.Errors.Select(e => new ApplicationException(e.Description)).ToList()
      );
    }

    var createdUser = await _userManager.FindByEmailAsync(user.Email!);

    if (createdUser == null)
    {
      throw new ApplicationException("Unable to find created user");
    }

    return createdUser;
  }

  public async Task<ApplicationUser?> GetUserByIdAsync(string id)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
  }
}