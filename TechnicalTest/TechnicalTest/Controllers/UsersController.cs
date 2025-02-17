using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Core.DTOs;
using Test.Core.Interfaces;
using Test.Core.Validators;

namespace TechnicalTest.Controllers
{
    [ApiController]
  
    public class UsersController : ControllerBase
    { 
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<Users>
        [HttpGet]
        [Route("[controller]/get-users")]

        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using a logging framework)
                return StatusCode(500, "Internal server error"); // General error response
            }
        }


        [HttpPost]
        [Route("[controller]/create-user")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            try
            {
                // Validate the userDto
                var validationResult = await new UserValidator().ValidateAsync(userDto);

                if (!validationResult.IsValid)
                {
                    // Return a BadRequest with validation errors
                    return BadRequest(validationResult.Errors);
                }

                userDto.Id = Guid.NewGuid(); // Generate a new Guid for the user
                var createdUser = await _userService.CreateUserAsync(userDto);

                return CreatedAtAction(nameof(GetUsers), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using a logging framework)
                return StatusCode(500, "Internal server error"); // General error response
            }
        }


        [HttpPost]
        [Route("[controller]/update-user")]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
        {
            try
            {
                // Validate the userDto
                var validationResult = await new UserValidator().ValidateAsync(userDto);

                if (!validationResult.IsValid)
                {
                    // Return a BadRequest with validation errors
                    return BadRequest(validationResult.Errors);
                }

                await _userService.UpdateUserAsync(userDto);
                return NoContent(); // Return No Content response on success
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using a logging framework)
                return StatusCode(500, "Internal server error"); // General error response
            }
        }

        [HttpDelete]
        [Route("[controller]/delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // User not found
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error"); // General error
            }
        }
    }
}
