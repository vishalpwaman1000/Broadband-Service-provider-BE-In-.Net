using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interface
{
    public interface ICustomerRL
    {
        public Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role);
        public Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID);
    }
}
