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
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _categoryBusiness;
        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [Route("get"), HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _categoryBusiness.GetAsync());
        }

        [Route("edit"), HttpPatch]
        public async Task<IActionResult> UpdateAsync([FromBody]Category category)
        {
            await _categoryBusiness.EditAsync(category);
            return Ok();
        }

        [Route("create"), HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Category category)
        {
            await _categoryBusiness.AddAsync(category);
            return Ok();
        }

        [Route("delete"), HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody]Category category)
        {
            await _categoryBusiness.DeleteAsync(category);
            return Ok();
        }

        [Route("getProduct"), HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int productId)
        {
            return Ok(await _categoryBusiness.GetAsync(productId));
        }

        [Route("getParentCategories"), HttpGet]
        public async Task<IActionResult> GetParentCategoriesAsync()
        {
            return Ok(await _categoryBusiness.GetParentCategoriesAsync());
        }
    }
}