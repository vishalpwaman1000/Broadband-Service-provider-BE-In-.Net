using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interface
{
    public interface IAuthenticationRL
    {
        public Task<SignInResponse> SignIn(SignInRequest request);
        public Task<SignUpResponse> SignUp(SignUpRequest request);
    }
}
