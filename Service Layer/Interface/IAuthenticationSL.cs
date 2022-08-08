using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Interface
{
    public interface IAuthenticationSL
    {
        public Task<SignInResponse> SignIn(SignInRequest request);
        public Task<SignUpResponse> SignUp(SignUpRequest request);
    }
}
