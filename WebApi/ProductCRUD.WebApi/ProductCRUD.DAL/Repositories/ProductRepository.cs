using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;

namespace ProductCRUD.DAL.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IProductDbContext productDbContext) : base(productDbContext)
        {
        }

        public Task AddAsync(Product product)
        {
            return _dbContext.Add(product);
        }

        public Task DeleteAsync(Product product)
        {
            return _dbContext.Delete(product);
        }

        public Task EditAsync(Product product)
        {
            return _dbContext.Edit(product);
        }

        public Task<Product> GetAsync(int id)
        {
            return _dbContext.ProductQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetAsync(string nameDescription, int categoryId = 0)
        {
            return _dbContext.ProductCategoryQuery.Include(x => x.Product)
                .Where(x =>
                 (categoryId == 0 || x.CategoryId == categoryId) && 
                 (string.IsNullOrEmpty(nameDescription) || 
                 (x.Product.Name.Contains(nameDescription) || x.Product.Description.Contains(nameDescription))))
                .Select(x => x.Product).Distinct().ToListAsync();
        }
    }
}