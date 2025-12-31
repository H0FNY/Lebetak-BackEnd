using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lebetak.DTOs
{
    public class AccountRegisterDTO
    {
        [Required]
        public string User_Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Confirm_Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone_Number { get; set; }

        public IFormFile? profileImage { get; set; }


        public string latitude { get; set; }
        public string longitude { get; set; }
        public string Role { get; set; }

        [Required]
        [Display(Name = "Enter Your First Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Enter Your Last Name")]
        public string Last_Name { get; set; }

        #region Location
        public string LocationURL { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        #endregion
        [Required]
        public IFormFile SSN_FrontImageURL { get; set; }
      
        
        [Required]
        public IFormFile SSN_BackImageURL { get; set; }

    }
}
