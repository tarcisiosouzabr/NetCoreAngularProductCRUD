using ProductCRUD.Business.Exceptions;
using ProductCRUD.Business.Infra;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
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

        public Task AddAsync(Category category)
        {
            return _categoryRepository.AddAsync(category);
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
            await _categoryRepository.EditAsync(currentCategory);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Category>> GetAsync()
        {
            return _categoryRepository.GetAsync();
        }
    }
}
