namespace Lebetak.DTOs
{
    public class UpdateUserInfoDTO
    {
            public string? F_Name { get; set; }
            public string? L_Name { get; set; }
            public string? PhoneNumber { get; set; }
            public IFormFile? ProfileImage { get; set; }
        
    }
}
