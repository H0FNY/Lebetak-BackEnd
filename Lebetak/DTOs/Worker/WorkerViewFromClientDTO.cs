using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class WorkerCardsDTO
    {
        // Worker Basic Info
        public string Description { get; set; }
        public int ExperienceYears { get; set; }
        public int HourlyRate { get; set; }
        public bool IsOnline { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsBooked { get; set; }

        // User Info
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }

        // Location
        public string Location { get; set; }

        // Category Info
        public string CategoryName { get; set; }

        // Skills
        public List<string> Skills { get; set; }

    }
}
