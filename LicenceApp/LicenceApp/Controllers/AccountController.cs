using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly LicenceDBContext _context;
    private readonly TokenService _tokenService;

    public AccountController(LicenceDBContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
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
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb is null)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, userInDb.PasswordHash, userInDb.PasswordSalt))
        {
            return BadRequest("Password is incorrect.");
        }

        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Token = accessToken,
        });
    }

    private bool VerifyPasswordHash(string? password, byte[]? passwordHash, byte[]? passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    private void CreatePasswordHash(string? password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

}
