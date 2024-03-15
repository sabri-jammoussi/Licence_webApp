using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services.Security;
using LicenceApp.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly LicenceDBContext _context;
    private readonly TokenService  _tokenService;
    private readonly IUserService _userService;


    public AccountController(LicenceDBContext context, TokenService tokenService,IUserService iuserService)
    {
        _context = context;
        _tokenService = tokenService;
        _userService= iuserService;
    }


    [HttpPost("register")]
    [SwaggerOperation(Tags = ["[Authentication]"])]

    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (userInDb != null)
        {
            return BadRequest("Email is already registered.");
        }

        // Generate password hash and salt
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = new UserDao
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = request.Role,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok("User successfully created!");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation(Tags = ["[Authentication]"])]

    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb is null)
        {
            return BadRequest("Email ou Mot de passe invalid !");
        }

        if (!VerifyPasswordHash(request.Password, userInDb.PasswordHash, userInDb.PasswordSalt))
        {
            return BadRequest("Email ou Mot de passe invalid !");
        }

        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Token = accessToken,
        });
    }
    
    [AllowAnonymous]
    [HttpPost("{id}")]
    [SwaggerOperation(Tags = ["[Update Profile]"])]

    public async Task<ActionResult> ModifierProfile(int id, UpdateProfile updateProfile)
    {
        try
        {
            await _userService.UpdateProfile(id, updateProfile);
            return Ok("UserInfo updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating user: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
    [AllowAnonymous]

    [HttpPost("Password")]
    [SwaggerOperation(Tags = ["[Password]"])]
    public async Task<ActionResult> ModifierProfilePassword(UpdateProfilePassword updateProfilepassword)
    {
        try
        {
            await _userService.UpdateProfilePassword(updateProfilepassword);
            return Ok("User Password updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating user: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("verify-password")]
    [SwaggerOperation(Tags = ["[Password]"])]

    public async Task<IActionResult> VerifyPassword([FromQuery] VerifyPasswordDto verifyPasswordDto)
    {
        var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == verifyPasswordDto.Id);

        if (existingUser == null)
            return BadRequest($"The Id : {verifyPasswordDto.Id} is invalid ");

        bool isPasswordCorrect = VerifyPasswordHash(verifyPasswordDto.CurrentPassword, existingUser.PasswordHash, existingUser.PasswordSalt);

        return Ok(isPasswordCorrect);
    }
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
 

}
