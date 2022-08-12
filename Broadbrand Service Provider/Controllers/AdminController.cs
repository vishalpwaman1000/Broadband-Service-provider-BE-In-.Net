using Common_Layer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broadbrand_Service_Provider.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerSL _customerSL;
        private readonly IAdminSL _adminSL;
        public AdminController(ICustomerSL customerSL, IAdminSL adminSL)
        {
            _customerSL = customerSL;
            _adminSL = adminSL;
        }

        [HttpPatch]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAdminAddress(UpdateCustomerAddressRequest request)
        {
            UpdateCustomerAddressResponse response = new UpdateCustomerAddressResponse();
            try
            {
                response = await _customerSL.UpdateCustomerAddress(request,"admin");

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAdminAddress([FromQuery] int UserID)
        {
            GetCustomerAddressResponse response = new GetCustomerAddressResponse();
            try
            {
                response = await _customerSL.GetCustomerAddress(UserID);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct([FromForm]AddProductRequest request)
        {
            AddProductResponse response = new AddProductResponse();
            try
            {
                response = await _adminSL.AddProduct(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPatch]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct([FromForm]UpdateProductRequest request)
        {
            UpdateProductResponse response = new UpdateProductResponse();
            try
            {
                response = await _adminSL.UpdateProduct(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllProduct(GetAllProductRequest request)
        {
            GetAllProductResponse response = new GetAllProductResponse();
            try
            {
                response = await _adminSL.GetAllProduct(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(DeleteProductRequest request)
        {
            DeleteProductResponse response = new DeleteProductResponse();
            try
            {
                response = await _adminSL.DeleteProduct(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

    }
}
