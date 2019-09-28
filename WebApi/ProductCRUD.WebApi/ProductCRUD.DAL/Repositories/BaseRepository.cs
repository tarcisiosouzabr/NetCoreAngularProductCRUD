using ProductCRUD.DAL.Infra;

namespace ProductCRUD.DAL.Repositories
{
    public abstract class BaseRepository
    {
        protected IProductDbContext _dbContext;
        public BaseRepository(IProductDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }
    }
}
