using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonBackendAspNet.Data;
using MonBackendAspNet.Models;
using MonBackendAspNet.Helpers;
using MonBackendAspNet.DTOs.User;


namespace MonBackendAspNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Utilisateur introuvable." });
            }

            if (!string.IsNullOrWhiteSpace(updatedUser.Username))
                user.Username = updatedUser.Username;

            if (!string.IsNullOrWhiteSpace(updatedUser.Email))
                user.Email = updatedUser.Email;

            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                if (!PasswordValidator.IsPasswordValid(updatedUser.Password))
                {
                    return BadRequest(new { message = "Le mot de passe ne respecte pas les critères de sécurité." });
                }

                user.Password = updatedUser.Password;
            }

            await _context.SaveChangesAsync();
            return Ok(user);
        }



        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
