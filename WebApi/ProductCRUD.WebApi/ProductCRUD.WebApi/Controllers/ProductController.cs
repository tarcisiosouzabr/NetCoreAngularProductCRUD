using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Business.Infra;
using ProductCRUD.Entities;
using ProductCRUD.WebApi.Request;
using ProductCRUD.WebApi.Services;

namespace ProductCRUD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBusiness;
        private IAzureStorageService _azureStorageService;
        public ProductController(IProductBusiness productBusiness, IAzureStorageService azureStorageService)
        {
            _productBusiness = productBusiness;
            _azureStorageService = azureStorageService;
        }

        [Route("get"), HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string nameDescription, [FromQuery]int categoryId = 0)
        {
            return Ok(await _productBusiness.GetAsync(nameDescription, categoryId));
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
            return Ok(product);
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

        [Route("uploadfile"), HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromBody]ProductImageRequest productImageRequest)
        {
            try
            {
                var fileName = await _azureStorageService.UploadFile(productImageRequest.ImageBase64);
                await _productBusiness.AddProductFileAsync(fileName, productImageRequest.ProductId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("getimages"), HttpGet]
        public async Task<IActionResult> GetProductImagesAsync([FromQuery]int productId)
        {
            return Ok(await _productBusiness.GetProductImagesAsync(productId));
        }

        [Route("deleteimage"), HttpDelete]
        public async Task<IActionResult> DeleteProductImageAsync([FromBody]ProductImage productImage)
        {
            await _productBusiness.DeleteProductImageAsync(productImage);
            return Ok();
        }
    }
}