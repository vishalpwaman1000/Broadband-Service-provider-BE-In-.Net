using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class GetCustomerAddressResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserPersonalDetail data { get; set; }
    }
/*    public class GetCustomerAddress
    {
        public int PersonalDetailID { get; set; }
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
*/
}

