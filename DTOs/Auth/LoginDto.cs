using System.ComponentModel.DataAnnotations;


namespace MonBackendAspNet.DTOs.Auth

{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
