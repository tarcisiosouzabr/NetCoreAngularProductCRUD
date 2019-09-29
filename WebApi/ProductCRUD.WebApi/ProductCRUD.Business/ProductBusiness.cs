using ProductCRUD.Business.Exceptions;
using ProductCRUD.Business.Infra;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository _productRepository;
        private IProductDbContext _dbContext;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductImageRepository _productImageRepository;
        public ProductBusiness(IProductRepository productRepository, IProductDbContext dbContext, IProductCategoryRepository productCategoryRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _dbContext = dbContext;
            _productCategoryRepository = productCategoryRepository;
            _productImageRepository = productImageRepository;
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

        public Task<List<Product>> GetAsync(string nameDescription, int categoryId = 0)
        {
            return _productRepository.GetAsync(nameDescription, categoryId);
        }

        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            await _productCategoryRepository.AddProductCategory(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductCategoryAsync(ProductCategory productCategory)
        {
            await _productCategoryRepository.DeleteAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int productId, int[] categories)
        {
            var currentCategories = await _productCategoryRepository.GetProductCategories(productId);
            var categoriesToDelete = currentCategories.Where(x => categories.Contains(x.CategoryId) == false).ToList();
            foreach (var item in categoriesToDelete)
            {
                await _productCategoryRepository.DeleteAsync(item);
            }
            var categoriesToAdd = categories.Where(x => currentCategories.Select(y => y.CategoryId).ToArray().Contains(x) == false).ToList();
            foreach (var item in categoriesToAdd)
            {
                await _productCategoryRepository.AddProductCategory(new ProductCategory { CategoryId = item, ProductId = productId });
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductFileAsync(Guid fileName, int productId)
        {
            await _productImageRepository.AddAsync(new ProductImage { Id = fileName, ProductId = productId });
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<ProductImage>> GetProductImagesAsync(int productId)
        {
            return _productImageRepository.GetAsync(productId);
        }

        public async Task DeleteProductImageAsync(ProductImage productImage)
        {
            await _productImageRepository.DeleteAsync(productImage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
