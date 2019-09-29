using Microsoft.EntityFrameworkCore;
using ProductCRUD.DAL.Infra;
using ProductCRUD.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.DAL
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        private DbSet<Product> Product { get; set; }
        public IQueryable<Product> ProductQuery { get { return Product; } }

        private DbSet<Category> Category { get; set; }
        public IQueryable<Category> CategoryQuery { get { return Category; } }

        private DbSet<ProductCategory> ProductCategory { get; set; }
        public IQueryable<ProductCategory> ProductCategoryQuery { get { return ProductCategory; } }

        private DbSet<ProductImage> ProductImage { get; set; }
        public IQueryable<ProductImage> ProductImageQuery { get { return ProductImage; } }

        public ProductDbContext(DbContextOptions op) : base(op)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
            .HasKey(o => new { o.CategoryId, o.ProductId });
            base.OnModelCreating(modelBuilder);
        }

        public Task Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return Task.Run(() =>
            {
                base.Remove(entity);
            });
        }

        public Task Edit<TEntity>(TEntity entity) where TEntity : class
        {
            return Task.Run(() =>
            {
                base.Update(entity);
            });
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task Add<TEntity>(TEntity entity) where TEntity : class
        {
            return Task.Run(() =>
            {
                base.Add(entity);
            });
        }
    }
}
