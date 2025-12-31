using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class ClientUpdateDTO
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }

        public IFormFile? profileImage { get; set; }




        [Display(Name = "Enter Your First Name")]
        public string F_Name { get; set; }


        [Display(Name = "Enter Your Last Name")]
        public string L_Name { get; set; }

        #region Location
        public string LocationURL { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Srteet { get; set; }
        public string ApartmentNumber { get; set; }
        #endregion
    }
}
