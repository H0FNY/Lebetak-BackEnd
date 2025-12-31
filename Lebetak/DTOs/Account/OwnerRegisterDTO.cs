using Lebetak.Models;

namespace Lebetak.DTOs.Account
{
    public class OwnerRegisterDTO : AccountRegisterDTO
    {
        public string CompaneyName { get; set; }
        public IFormFile? LogoImageUrl { get; set; }
        public IFormFile? PannerUrl { get; set; }
        public string Descreption { get; set; }
        public int CategoryId { get; set; } 

    }
}
