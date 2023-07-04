using Microsoft.AspNetCore.Mvc;
using SudeMerk.OrderService.Model.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SudeMerk.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            string userId = "1";
            var orders = _orderService.GetOrderForUser(userId);
            return Ok(orders); 
        }

        // GET api/<OrderController>/5
        [HttpGet("{OrderId}")]
        public IActionResult Get(Guid id)
        {
           var order = _orderService.GetOrderById(id);
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] AddOrderDto value)
        {
            _orderService.AddOrder(value);
            return Ok();
        }
    }
}
