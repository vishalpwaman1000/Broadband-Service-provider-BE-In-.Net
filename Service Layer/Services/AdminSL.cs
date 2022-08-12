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
    public class AdminSL : IAdminSL
    {
        public readonly IAdminRL _adminRL;
        public AdminSL(IAdminRL adminRL)
        {
            _adminRL = adminRL;
        }

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            return await _adminRL.AddProduct(request);
        }

        public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request)
        {
            return await _adminRL.DeleteProduct(request);
        }

        public async Task<GetAllProductResponse> GetAllProduct(GetAllProductRequest request)
        {
            return await _adminRL.GetAllProduct(request);
        }

        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            return await _adminRL.UpdateProduct(request);
        }
    }
}
