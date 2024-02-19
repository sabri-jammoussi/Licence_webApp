using Azure.Core;
using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services;
using LicenceApp.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
      

        }
        //[Authorize]
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
