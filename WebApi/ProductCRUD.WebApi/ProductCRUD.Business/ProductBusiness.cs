using ProductCRUD.Business.Exceptions;
using ProductCRUD.Business.Infra;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository _productRepository;
        private IProductDbContext _dbContext;
        private IProductCategoryRepository _productCategoryRepository;
        public ProductBusiness(IProductRepository productRepository, IProductDbContext dbContext, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _dbContext = dbContext;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task AddAsync(Product product)
        {
            product.CreatedDate = DateTime.Now;
            await _productRepository.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            var currentProduct = await _productRepository.GetAsync(product.Id);
            if (currentProduct == null)
                throw new BusinessException("Produto não encontrado.");
            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.Price = product.Price;
            currentProduct.Active = product.Active;
            await _productRepository.EditAsync(currentProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            var currentProduct = await _productRepository.GetAsync(product.Id);
            if (currentProduct == null)
                throw new BusinessException("Produto não encontrado.");
            await _productRepository.DeleteAsync(currentProduct);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Product>> GetAsync()
        {
            return _productRepository.GetAsync();
        }

        public Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            return _productCategoryRepository.AddProductCategory(productCategory);
        }
    }
}
