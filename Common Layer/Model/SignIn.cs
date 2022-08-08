using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class SignInResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public SignIn data { get; set; }
        public string Token { get; set; }
    }

    public class SignIn
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    } 
}
