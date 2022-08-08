using Common_Layer;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class CustomerRL : ICustomerRL
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CustomerRL(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID)
        {
            GetCustomerAddressResponse response = new GetCustomerAddressResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Data Successful";

            try
            {

                var GetCustomerAddress = _applicationDbContext.UserPersonalDetails
                    .Where(X => X.UserID == UserID)
                    .FirstOrDefault();

                if(GetCustomerAddress == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User Data Not Found";
                    return response;
                }

                response.data = new UserPersonalDetail();
                response.data = GetCustomerAddress;

                /*
                response.data = new UserPersonalDetail();
                response.data.Address = GetCustomerAddress.Address;
                response.data.Address = GetCustomerAddress.Address;
                response.data.City = GetCustomerAddress.City;
                response.data.CompanyName = GetCustomerAddress.CompanyName;
                response.data.DOB = GetCustomerAddress.DOB;
                response.data.Email = GetCustomerAddress.Email;
                response.data.FirstName = GetCustomerAddress.FirstName;
                response.data.Gender = GetCustomerAddress.Gender;
                response.data.HomePhone = GetCustomerAddress.HomePhone;
                response.data.LastName = GetCustomerAddress.LastName;
                response.data.Occupation = GetCustomerAddress.Occupation;
                response.data.PersonalDetailID = GetCustomerAddress.PersonalDetailID;
                response.data.PersonalNumber = GetCustomerAddress.PersonalNumber;
                response.data.Position = GetCustomerAddress.Position;
                response.data.State = GetCustomerAddress.State;
                response.data.UserID = GetCustomerAddress.UserID;
                response.data.ZipCode = GetCustomerAddress.ZipCode;
                */

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role)
        {
            UpdateCustomerAddressResponse response = new UpdateCustomerAddressResponse();
            response.IsSuccess = true;
            response.Message = "Update Customer Detail Successfully";

            try
            {

                var UserPersonalDetail = _applicationDbContext.UserPersonalDetails.Where(X => X.UserID == request.UserID).FirstOrDefault();
                if (UserPersonalDetail == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong";
                    return response;
                }

                //UserID FirstName LastName Address City State ZipCode HomePhone 
                //PersonalNumber Email Gender DOB Occupation CompanyName Position
                UserPersonalDetail.Address = request.Address;
                UserPersonalDetail.City = request.City;
                UserPersonalDetail.State = request.State;
                UserPersonalDetail.ZipCode = request.ZipCode;
                UserPersonalDetail.HomePhone = request.HomePhone;
                UserPersonalDetail.PersonalNumber = request.PersonalNumber;
                UserPersonalDetail.DOB = request.DOB;
                UserPersonalDetail.Email = request.Email;
                UserPersonalDetail.Gender = request.Gender;
                
                if(Role != "customer")
                {
                    UserPersonalDetail.Position = request.Position;
                }
                else
                {
                    UserPersonalDetail.Occupation = request.Occupation;
                    UserPersonalDetail.CompanyName = request.CompanyName;
                }

                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Update Customer Detail Failed";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }
    }
}
