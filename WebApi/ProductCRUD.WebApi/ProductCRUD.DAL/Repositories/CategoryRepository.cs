using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.DAL.Infra;
using ProductCRUD.DAL.Infra.Repositories;
using ProductCRUD.Entities;

namespace ProductCRUD.DAL.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IProductDbContext productDbContext) : base(productDbContext)
        {
        }

        public Task AddAsync(Category category)
        {
            return _dbContext.Add(category);
        }

        public Task DeleteAsync(Category category)
        {
            return _dbContext.Delete(category);
        }

        public Task EditAsync(Category category)
        {
            return _dbContext.Edit(category);
        }

        public Task<Category> GetAsync(int id)
        {
            return _dbContext.CategoryQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Category>> GetAsync()
        {
            return _dbContext.CategoryQuery.ToListAsync();
        }
    }
}
