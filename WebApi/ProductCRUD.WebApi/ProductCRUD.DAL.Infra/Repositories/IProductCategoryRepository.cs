using ProductCRUD.Entities;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Infra.Repositories
{
    public interface IProductCategoryRepository
    {
        Task AddProductCategory(ProductCategory productCategory);
    }
}
