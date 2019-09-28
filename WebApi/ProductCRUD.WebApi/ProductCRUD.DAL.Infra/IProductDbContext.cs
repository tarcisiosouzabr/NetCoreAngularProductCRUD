using ProductCRUD.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Infra
{
    public interface IProductDbContext
    {
        Task<int> SaveChangesAsync();
        Task Add<TEntity>(TEntity entity) where TEntity : class;
        Task Edit<TEntity>(TEntity entity) where TEntity : class;
        Task Delete<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<Product> ProductQuery { get; }
        IQueryable<Category> CategoryQuery { get; }
        IQueryable<ProductCategory> ProductCategoryQuery { get; }
    }
}
