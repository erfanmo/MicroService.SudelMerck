using Microsoft.EntityFrameworkCore;
using SudelMerck.BasketService.Model.Entities;

namespace SudelMerck.BasketService.Infrastracture.Context
{
    public class BasketDataBaseContext : DbContext
    {
        public BasketDataBaseContext(DbContextOptions<BasketDataBaseContext> option) : base(option)
        {
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> Items { get; set; }
    }
}
