using Microsoft.EntityFrameworkCore;
using SudelMerck.DiscountService.Model.Entities;

namespace SudelMerck.DiscountService.Infrastructure.Context
{
    public class DiscountDataBaseContext : DbContext
    {
        public DiscountDataBaseContext(DbContextOptions<DiscountDataBaseContext> option) : base(option)
        {

        }
        public DbSet<Discount> Discount { get; set; }
    }
}
