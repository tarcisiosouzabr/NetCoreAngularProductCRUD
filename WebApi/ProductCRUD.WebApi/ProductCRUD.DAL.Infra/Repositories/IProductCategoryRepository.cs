using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Infra.Repositories
{
    public interface IProductCategoryRepository
    {
        Task AddProductCategory(ProductCategory productCategory);
        Task DeleteAsync(ProductCategory productCategory);
        Task<List<ProductCategory>> GetProductCategories(int productId);
    }
}
