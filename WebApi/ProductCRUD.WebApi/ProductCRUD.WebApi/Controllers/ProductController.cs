using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Business.Infra;
using ProductCRUD.Entities;

namespace ProductCRUD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [Route("get"), HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string name)
        {
            return Ok(await _productBusiness.GetAsync());
        }

        [Route("edit"), HttpPatch]
        public async Task<IActionResult> UpdateAsync([FromBody]Product product)
        {
            await _productBusiness.EditAsync(product);
            return Ok();
        }

        [Route("create"), HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Product product)
        {
            await _productBusiness.AddAsync(product);
            return Ok();
        }

        [Route("delete"), HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody]Product product)
        {
            await _productBusiness.DeleteAsync(product);
            return Ok();
        }
    }
}