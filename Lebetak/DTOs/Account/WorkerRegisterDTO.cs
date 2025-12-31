using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs.Account
{
    public class WorkerRegisterDTO : AccountRegisterDTO
    {

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int HourlyPrice { get; set; }
        public int ExperienceYears { get; set; }

        public List<string>? WorkerSkills { get; set; }



    }                    
}
