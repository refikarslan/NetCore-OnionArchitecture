using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_OnionArchitecture.Application.DTOs;
using NetCore_OnionArchitecture.Application.Features.Customers;
using NetCore_OnionArchitecture.Application.Wrappers;

namespace NetCore_OnionArchitecture.WebApi.Controllers.v1.Customer
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseApiController
    {
        private CustomersManager _customersManager;

        public CustomerController(CustomersManager customersManager)
        {
            _customersManager = customersManager;
        }

      
        [HttpGet("getCustomer")]
        public async Task<ActionResult<Response<List<CustomerDto>>>> GetCustomers()
        {
            var res = await _customersManager.GetCustomers();
            return Ok(new Response<List<CustomerDto>>(data: res));
        }

        
        [HttpGet("getIdCustomer")]
        public async Task<ActionResult<Response<CustomerDto>>> GetCustomerForEdit(int customerId)
        {
            var res = await _customersManager.GetCustomerForEdit(customerId);
            return Ok(new Response<CustomerDto>(data: res));
        }
         
       
        [HttpPost("customerAddOrEdit")]
        public async Task<ActionResult<Response<int>>> AddOrEditCustomer(CustomerDto input)
        {
            var res = await _customersManager.AddOrEditCustomer(input);
            return Ok(new Response<int>(data: res));
        }

        [HttpDelete("customerDelete")]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            await _customersManager.DeleteCustomer(customerId);
            return Ok();
        }
    }
}
