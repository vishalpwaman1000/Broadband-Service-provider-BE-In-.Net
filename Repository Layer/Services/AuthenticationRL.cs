using Common_Layer;
using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository_Layer.Interface
{
    public class AuthenticationRL : IAuthenticationRL
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthenticationRL(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Login In Successfully";

            try
            {

                var UserDetails = _applicationDbContext.UserDetails
                    .Where(X => X.Email == request.Email.ToLower() &&
                                       X.Password == Encrypt(request.Password))
                    .FirstOrDefault();

                if (UserDetails == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid UserName And Password";
                    return response;
                }

                response.data = new SignIn();
                response.data.UserID = UserDetails.UserID;
                response.data.Email = UserDetails.Email;
                response.data.FullName = UserDetails.FirstName + " " + UserDetails.LastName;
                response.data.Role = UserDetails.Role.ToLower();

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Registration Successfully";

            try
            {

                // Check Email Id Already Present Or not
                var UserDetail = _applicationDbContext.UserDetails
                    .Where(X => X.Email == request.Email).FirstOrDefault();
                if (UserDetail != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Email Id Already Present";
                    return response;
                }

                if (request.Role.ToLower() != "customer" && request.MasterPassword != "India@123")
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Master Password";
                    return response;
                }

                // Create New Account
                UserDetails userDetails = new UserDetails()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email.ToLower(),
                    DateTime = DateTime.Now,
                    Password = Encrypt(request.Password),
                    Role = request.Role.ToLower(),
                    IsActive = true
                };

                await _applicationDbContext.AddAsync(userDetails);
                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong At UserDetail Tables";
                    return response;
                }

                var FetchUserData = _applicationDbContext.UserDetails
                    .Where(X => X.Email == request.Email)
                    .FirstOrDefault();

                UserPersonalDetail userPersonalDetail = new UserPersonalDetail()
                {
                    UserID = FetchUserData.UserID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                };

                await _applicationDbContext.AddAsync(userPersonalDetail);
                Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong At UserPersonalDetail Tables";
                    return response;
                }

                Thread.Sleep(1000);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        private static string Encrypt(string originalString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

    }
}
