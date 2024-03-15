using LicenceApp.models;
using LicenceApp.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicenceApp.Controllers
{
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
      

        }
       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDao>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserbyId(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Roles")]
        public async Task<ActionResult> GetUserRole()
        {
            try
            {
                var user = await _userService.GetUserRole();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PostUser(NewUser newUser)
        {
          

            try
            {
                await _userService.Create(newUser);
                return Ok("user Added ");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, UpdateUser user)
        {
            try
            {
                await _userService.Update(id, user); // Simulating an asynchronous operation
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating user: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
     
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUserbyId(id);
                if (user != null)
                    await _userService.Delete(id);
                return Ok("user deleted ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
