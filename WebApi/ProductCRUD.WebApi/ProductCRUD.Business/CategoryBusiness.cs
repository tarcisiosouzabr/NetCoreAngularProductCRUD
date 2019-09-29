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
    public class CategoryBusiness : ICategoryBusiness
    {
        private ICategoryRepository _categoryRepository;
        private IProductDbContext _dbContext;
        public CategoryBusiness(ICategoryRepository categoryRepository, IProductDbContext dbContext)
        {
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category category)
        {
            category.CreatedDate = DateTime.Now;
            if (category.ParentId <= 0)
                category.ParentId = null;
            await _categoryRepository.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            var currentCategory = await _categoryRepository.GetAsync(category.Id);
            if (currentCategory == null)
                throw new BusinessException("Categoria não encontrada.");
            await _categoryRepository.DeleteAsync(currentCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Category category)
        {
            var currentCategory = await _categoryRepository.GetAsync(category.Id);
            if (currentCategory == null)
                throw new BusinessException("Categoria não encontrada.");
            currentCategory.Name = category.Name;
            if (category.ParentId > 0)
                currentCategory.ParentId = category.ParentId;
            await _categoryRepository.EditAsync(currentCategory);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Category>> GetAsync()
        {
            return _categoryRepository.GetAsync();
        }

        public Task<List<Category>> GetAsync(int productId)
        {
            return _categoryRepository.GetProductCategoriesAsync(productId);
        }

        public Task<List<Category>> GetParentCategoriesAsync()
        {
            return _categoryRepository.GetParentCategoriesAsync();
        }
    }
}
