using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Business.Infra;
using ProductCRUD.Entities;
using ProductCRUD.WebApi.Request;

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

        [Route("updatecategory"), HttpPost]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody]UpdateCategoryRequest updateCategoryRequest)
        {
            await _productBusiness.UpdateCategoryAsync(updateCategoryRequest.ProductId, updateCategoryRequest.Categories);
            return Ok();
        }

        [Route("deletecategory"), HttpDelete]
        public async Task<IActionResult> DeleteProductCategoryAsync([FromBody]ProductCategory productCategory)
        {
            await _productBusiness.RemoveProductCategoryAsync(productCategory);
            return Ok();
        }
    }
}