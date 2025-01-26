using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore_OnionArchitecture.Application.DTOs.Product;
using NetCore_OnionArchitecture.Application.Features.Customers;
using NetCore_OnionArchitecture.Application.Features.Products;
using NetCore_OnionArchitecture.Application.Wrappers;

namespace NetCore_OnionArchitecture.WebApi.Controllers.v1.Product
{
    [Route("[controller]/[action]")]
    [ApiController]
    
    public class ProductController : BaseApiController
    {
        private ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

       
        [HttpGet("GetProduct")]
        public async Task<ActionResult<Response<ProductDto>>> GetProducts()
        {
            var res = await _productManager.GetProducts();
            return Ok(new Response<List<ProductDto>>(data: res));
        }




    }
}
