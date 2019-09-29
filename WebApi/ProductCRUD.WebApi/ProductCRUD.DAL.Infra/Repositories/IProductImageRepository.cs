using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Infra.Repositories
{
    public interface IProductImageRepository
    {
        Task AddAsync(ProductImage productImage);
        Task<List<ProductImage>> GetAsync(int productId);
        Task DeleteAsync(ProductImage productImage);
    }
}
