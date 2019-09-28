using ProductCRUD.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCRUD.DAL.Infra.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task EditAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category> GetAsync(int id);
        Task<List<Category>> GetAsync();
    }
}
