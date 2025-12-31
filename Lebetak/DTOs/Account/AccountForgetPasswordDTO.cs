using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class AccountForgetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
