using Common_Layer.Model;
using Repository_Layer.Interface;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services
{
    public class CustomerSL : ICustomerSL
    {
        private readonly ICustomerRL _customerRL;
        public CustomerSL(ICustomerRL customerRL)
        {
            _customerRL = customerRL;
        }

        public async Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID)
        {
            return await _customerRL.GetCustomerAddress(UserID);
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role)
        {
            return await _customerRL.UpdateCustomerAddress(request, Role);
        }
    }
}
