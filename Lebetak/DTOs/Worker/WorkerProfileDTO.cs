namespace Lebetak.DTOs.Worker
{
    public class WorkerProfileDTO
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Category_Title { get; set; }
        public string Description { get; set; }
        public int Experience_Years { get; set; }
        public int HourlyPrice { get; set; }
        public double Rate { get; set; }
        public int NumberOfRates { get; set; } = 0;
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string? ApartmentNumber { get; set; }
        public string Profile_Image { get; set; }

        public ICollection<string>? Skills { get; set; }
        public ICollection<ProjectCardDTO>? Projects { get; set; }
    }
}
