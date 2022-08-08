using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Interface
{
    public class AuthenticationSL : IAuthenticationSL
    {
        private readonly IAuthenticationRL _authenticationRL;
        public AuthenticationSL(IAuthenticationRL authenticationRL)
        {
            _authenticationRL = authenticationRL;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            return await _authenticationRL.SignIn(request);
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            return await _authenticationRL.SignUp(request);
        }
    }
}
