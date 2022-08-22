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

        public async Task<CreateTicketResponse> CreateTicket(CreateTicketRequest request)
        {
            return await _customerRL.CreateTicket(request);
        }

        public async Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID)
        {
            return await _customerRL.GetCustomerAddress(UserID);
        }

        public async Task<GetPurchaseProductResponse> GetPurchaseProduct(GetPurchaseProductRequest request)
        {
            return await _customerRL.GetPurchaseProduct(request);
        }

        public async Task<GetTicketHistoryResponse> GetTicketHistory(GetTicketHistoryRequest request)
        {
            return await _customerRL.GetTicketHistory(request);
        }

        public async Task<PurchaseProductResponse> PurchaseProduct(PurchaseProductRequest request)
        {
            return await _customerRL.PurchaseProduct(request);
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role)
        {
            return await _customerRL.UpdateCustomerAddress(request, Role);
        }
    }
}
