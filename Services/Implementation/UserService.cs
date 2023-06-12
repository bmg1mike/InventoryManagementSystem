using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IUserService
{
    Task<string> RegisterUser(AddUserDto request);
    Task<PaginationResponse<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserAsync(string userId);
}

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<string> RegisterUser(AddUserDto request)
    {
        try
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                CompanyName = request.CompanyName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return "User created successfully";
            }

            return "There was an error creating the user";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return string.Empty;
        }
    }

    public async Task<PaginationResponse<AppUser>> GetUsersAsync()
    {
        var users = await _unitOfWork.User.GetAllAsync();
        return users;
    }

    public async Task<AppUser> GetUserAsync(string userId)
    {
        var user = await _unitOfWork.User.GetByIdAsync(userId);
        return user;
    }
}