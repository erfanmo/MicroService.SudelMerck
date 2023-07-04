using Microsoft.EntityFrameworkCore;
using SudeMerk.OrderService.Infrastructure.Context;
using SudeMerk.OrderService.Model.Entities;

namespace SudeMerk.OrderService.Model.Service
{
    public interface IOrderService
    {
        void AddOrder(AddOrderDto order);

        List<OrderDto> GetOrderForUser(string UsrId);

        OrderDto GetOrderById(Guid id);

    }

    public class OrderService : IOrderService
    {
        private readonly OrderDataBaseContext _context;
        public OrderService(OrderDataBaseContext context)
        {
            this._context = context;
        }

        public void AddOrder(AddOrderDto addOrder)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach (var item in addOrder.OrderLine)
            {
                orderLines.Add(new OrderLine
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity,
                });
            }
            Order order = new Order(addOrder.UserId, orderLines);
            _context.Order.Add(order);
            _context.SaveChanges();
        }

        public OrderDto GetOrderById(Guid Id)
        {
            var order = _context.Order.Include(p => p.OrderLines).FirstOrDefault(x => x.Id == Id);

            if (order == null)
                throw new Exception("There is no Order with this Id ...!!!");
            var result = new OrderDto
            {
                Id = order.Id,
                OrderPaid = order.OrderPaid,
                OrderPlaced = order.OrderPlaced,
                UserId = order.UserId,
                OrderLines = (List<OrderLineDto>)order.OrderLines.Select(o => new OrderLineDto
                {
                    ProductId = o.ProductId,
                    ProductName = o.ProductName,
                    ProductPrice = o.ProductPrice,
                    Quantity = o.Quantity,
                }),
            };
            return result;
            
        }

        public List<OrderDto> GetOrderForUser(string UsrId)
        {
            var orders = _context.Order.Where(p => p.UserId == UsrId).Include(p => p.OrderLines)
                .Select(p => new OrderDto
                {
                    Id = p.Id,
                    OrderPaid = p.OrderPaid,
                    OrderPlaced = p.OrderPlaced,
                    OrderLines = (List<OrderLineDto>)p.OrderLines.Select(o=> new OrderLineDto
                    {
                        ProductId = o.ProductId,
                        ProductName = o.ProductName,
                        ProductPrice = o.ProductPrice,
                        Quantity = o.Quantity
                    }),

                }).ToList();
            return orders;
        }
    }

    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public bool OrderPaid { get; set; }
        public DateTime OrderPlaced { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
    }

    public class AddOrderDto
    {
        public string UserId { get; set; }
        public IEnumerable<OrderLineDto> OrderLine { get;  set; }
    }

    public class OrderLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice{ get; set; }
        public int Quantity { get; set; }
    }

  
}
