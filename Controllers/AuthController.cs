using Microsoft.AspNetCore.Mvc;
using MonBackendAspNet.Models;
using MonBackendAspNet.Data;
using MonBackendAspNet.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace MonBackendAspNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Un utilisateur avec cet email existe déjà." });

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
            };

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Compte créé avec succès." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Identifiants invalides." });

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Identifiants invalides." });

            return Ok(new { message = "Connexion réussie", userId = user.Id });
        }
    }
}
