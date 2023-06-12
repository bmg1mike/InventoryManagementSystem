namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(AddUserDto request)
    {
        return Ok(await _userService.RegisterUser(request));
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await _userService.GetUsersAsync());
    }

    [HttpGet("GetUserById/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        return Ok(await _userService.GetUserAsync(userId));
    }
}