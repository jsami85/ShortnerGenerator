using System.ComponentModel.DataAnnotations;

namespace SigmaTest.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
