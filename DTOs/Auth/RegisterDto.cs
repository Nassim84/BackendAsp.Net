using System.ComponentModel.DataAnnotations;

namespace MonBackendAspNet.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MinLength(10)]
        public string Password { get; set; } = string.Empty;
    }
}
