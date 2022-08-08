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
    public class TechnicianController : ControllerBase
    {

        private readonly ICustomerSL _customerSL;
        public TechnicianController(ICustomerSL customerSL)
        {
            _customerSL = customerSL;
        }

        [HttpPatch]
        [Authorize(Roles = "technician")]
        public async Task<IActionResult> UpdateTechnicianAddress(UpdateCustomerAddressRequest request)
        {
            UpdateCustomerAddressResponse response = new UpdateCustomerAddressResponse();
            try
            {
                response = await _customerSL.UpdateCustomerAddress(request, "technician");

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Mesage : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "technician")]
        public async Task<IActionResult> GetTechnicianAddress([FromQuery] int UserID)
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


    }
}
