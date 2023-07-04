using Microsoft.EntityFrameworkCore;
using SudeMerk.OrderService.Model.Entities;

namespace SudeMerk.OrderService.Infrastructure.Context
{
    public class OrderDataBaseContext:DbContext
    {
        public OrderDataBaseContext(DbContextOptions<OrderDataBaseContext> options):base(options)
        {

        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
    }
}
