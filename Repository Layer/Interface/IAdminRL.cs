using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interface
{
    public interface IAdminRL
    {
        public Task<AddProductResponse> AddProduct(AddProductRequest request);
        public Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request);
        public Task<GetAllProductResponse> GetAllProduct(GetAllProductRequest request);
        public Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request);
    }
}
