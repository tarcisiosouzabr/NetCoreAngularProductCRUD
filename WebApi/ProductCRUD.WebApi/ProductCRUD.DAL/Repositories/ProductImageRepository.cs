using Microsoft.EntityFrameworkCore;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Repositories
{
    public class ProductImageRepository : BaseRepository, IProductImageRepository
    {
        public ProductImageRepository(IProductDbContext productDbContext) : base(productDbContext)
        {
        }

        public Task AddAsync(ProductImage productImage)
        {
            return _dbContext.Add(productImage);
        }

        public Task DeleteAsync(ProductImage productImage)
        {
            return _dbContext.Delete(productImage);
        }

        public Task<List<ProductImage>> GetAsync(int productId)
        {
            return _dbContext.ProductImageQuery.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
