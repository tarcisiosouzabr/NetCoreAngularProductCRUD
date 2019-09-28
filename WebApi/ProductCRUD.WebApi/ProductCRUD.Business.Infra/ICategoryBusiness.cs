using ProductCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductCRUD.Business.Infra
{
    public interface ICategoryBusiness
    {
        Task AddAsync(Category category);
        Task EditAsync(Category category);
        Task DeleteAsync(Category category);
        Task<List<Category>> GetAsync();
    }
}
