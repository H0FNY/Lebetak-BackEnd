

using Microsoft.AspNetCore.Identity;

namespace Lebetak.Models
{ 
    public class User : IdentityUser
    {
        //String Id
        //Email
        //Username
        //Phone Number
        //Password Hash
        //Created At


        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string? profileImageUrl { get; set; }

        #region Location
        public string? LocationURL { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string? Street { get; set; }
        public string? ApartmentNumber { get; set; }
        #endregion
        public string? SSN_FrontURL { get; set; }
        public string? SSN_BackURL { get; set; }        

        // Navigation Properties
        public virtual Wallet? Wallet { get; set; }
        public virtual Client? Client { get; set; }
        public virtual Worker? Worker { get; set; }
        public virtual Owner? Owner { get; set; }

    }
}
