using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD.Business.Infra
{
    public interface IProductBusiness
    {
        Task AddAsync(Product product);
        Task EditAsync(Product product);
        Task DeleteAsync(Product product);
        Task<List<Product>> GetAsync(string nameDescription, int categoryId);
        Task AddProductCategoryAsync(ProductCategory productCategory);
        Task RemoveProductCategoryAsync(ProductCategory productCategory);
        Task UpdateCategoryAsync(int productId, int[] categories);
    }
}
