using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;
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
    }
}
