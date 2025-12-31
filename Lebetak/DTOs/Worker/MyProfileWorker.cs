namespace Lebetak.DTOs.Worker
{
    public class MyProfileWorker
    {
        //User Info
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Profile_Image { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string FSSN { get; set; }
        public string BSSN { get; set; }
        public decimal WallertBalance { get; set; }

        //Worker Info
        public string Description { get; set; }
        public int Experience_Years { get; set; }
        public double Rate { get; set; }
        public int NumberOfRates { get; set; }

        public int HourlyPrice { get; set; }
        public string Category_Title { get; set; }
        public List<string> Skills { get; set; }
        public ICollection<ProjectCardDTO>? Projects { get; set; }

    }
}
