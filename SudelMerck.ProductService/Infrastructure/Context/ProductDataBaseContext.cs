using Microsoft.EntityFrameworkCore;
using SudelMerck.ProductService.Model.Entity;

namespace SudelMerck.ProductService.Infrastructure.Context
{
    public class ProductDataBaseContext:DbContext
    {
        public ProductDataBaseContext(DbContextOptions<ProductDataBaseContext> options)
            :base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
