using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Interface
{
    public interface ICustomerSL
    {
        public Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role);
        public Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID);
        public Task<CreateTicketResponse> CreateTicket(CreateTicketRequest request);
        public Task<GetTicketHistoryResponse> GetTicketHistory(GetTicketHistoryRequest request);
        public Task<PurchaseProductResponse> PurchaseProduct(PurchaseProductRequest request);
        public Task<GetPurchaseProductResponse> GetPurchaseProduct(GetPurchaseProductRequest request);
    }
}
