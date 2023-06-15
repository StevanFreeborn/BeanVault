namespace BeanVault.Services.AuthService.Infrastructure.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  public UserService(
    IUserRepository userRepository,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager
  )
  {
    _userRepository = userRepository;
    _userManager = userManager;
    _roleManager = roleManager;
  }

  /// <Summary>
  /// Adds the given user.
  /// </Summary>
  /// <param name="user">The user to be added</param>
  /// <returns>The created user</returns>
  /// <exception cref="InvalidModelException"></exception>
  /// <exception cref="ApplicationException"></exception>
  /// <exception cref="AggregateException"></exception>
  public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
  {
    if (user.Email == null)
    {
      throw new InvalidModelException("No email provided for user");
    }

    var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);

    if (existingUser != null)
    {
      throw new InvalidModelException($"User already exists with email: {user.Email}");
    }

    var result = await _userRepository.AddUserAsync(user);

    if (result is null)
    {
      throw new ApplicationException("Unable to add new user");
    }

    if (result.Succeeded is false)
    {
      throw new AggregateException(
        result.Errors.Select(e => new ApplicationException(e.Description)).ToList()
      );
    }

    var createdUser = await _userRepository.GetUserByEmailAsync(user.Email!);

    if (createdUser is null)
    {
      throw new ApplicationException("Unable to find created user");
    }

    return createdUser;
  }

  /// <summary>
  /// Logs in the given user.
  /// </summary>
  /// <param name="user"></param>
  /// <returns></returns>
  /// <exception cref="InvalidModelException"></exception>
  /// <exception cref="InvalidLoginException"></exception>
  public async Task<ApplicationUser> LogInUserAsync(ApplicationUser user)
  {
    if (user.UserName == null)
    {
      throw new InvalidModelException("No username provided for user");
    }

    if (user.Password == null)
    {
      throw new InvalidModelException("No password provided for user");
    }

    var userLoggingIn = await _userRepository.GetUserByUsernameAsync(user.UserName);

    if (userLoggingIn is null)
    {
      throw new InvalidLoginException($"Invalid username or password");
    }

    var isCorrectPassword = await _userManager.CheckPasswordAsync(userLoggingIn, user.Password);

    if (isCorrectPassword is false)
    {
      throw new InvalidLoginException("Invalid username or password");
    }

    return userLoggingIn;
  }

  /// <summary>
  /// Gets a user with the given id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="ModelNotFoundException"></exception>
  public async Task<ApplicationUser> GetUserByIdAsync(string id)
  {
    var user = await _userRepository.GetUserByIdAsync(id);

    if (user is null)
    {
      throw new ModelNotFoundException($"Unable to find user with id: {id}");
    }

    return user;
  }

  /// <summary>
  /// Adds a role to the user. If the given role doesn't exist it will create it.
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="roleName"></param>
  /// <returns></returns>
  /// <exception cref="ModelNotFoundException"></exception>
  /// <exception cref="AggregateException"></exception>
  public async Task AddRoleToUserAsync(string userId, string roleName)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);

    if (user is null)
    {
      throw new ModelNotFoundException($"Unable to find user with id: {userId}");
    }

    var doesRoleExist = await _roleManager.RoleExistsAsync(roleName);

    if (doesRoleExist == false)
    {
      var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

      if (roleCreationResult.Succeeded == false)
      {
        throw new AggregateException(
          roleCreationResult.Errors.Select(e => new ApplicationException(e.Description)).ToList()
        );
      }
    }

    var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);

    if (roleAssignmentResult.Succeeded == false)
    {
      throw new AggregateException(
        roleAssignmentResult.Errors.Select(e => new ApplicationException(e.Description)).ToList()
      );
    }
  }

  public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
  {
    var roles = await _userManager.GetRolesAsync(user);

    return roles == null ? new List<string>() : roles;
  }
}