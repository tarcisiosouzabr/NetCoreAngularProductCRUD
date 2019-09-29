using Microsoft.EntityFrameworkCore;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Repositories
{
    public class ProductCategoryRepository : BaseRepository, IProductCategoryRepository
    {
        public ProductCategoryRepository(IProductDbContext productDbContext) : base(productDbContext)
        {
        }

        public Task AddProductCategory(ProductCategory productCategory)
        {
            return _dbContext.Add(productCategory);
        }

        public Task DeleteAsync(ProductCategory productCategory)
        {
            return _dbContext.Delete(productCategory);
        }

        public Task<List<ProductCategory>> GetProductCategories(int productId)
        {
            return _dbContext.ProductCategoryQuery.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
