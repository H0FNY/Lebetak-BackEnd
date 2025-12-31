using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class AccountVerfiyCodeDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Code { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

    }
}
