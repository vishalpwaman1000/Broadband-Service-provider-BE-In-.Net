using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class UpdateCustomerAddressRequest
    {
        //UserID FirstName LastName Address City State ZipCode HomePhone 
        //PersonalNumber Email Gender DOB Occupation CompanyName Position
        [Required]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string HomePhone { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
    }

    public class UpdateCustomerAddressResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
