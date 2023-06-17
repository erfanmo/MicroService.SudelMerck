using Microsoft.AspNetCore.Mvc;
using SudelMerck.BasketService.Model.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SudelMerck.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _basketServices;
        public BasketController(IBasketServices basketServices)
        {
            this._basketServices = basketServices;
        }
        // GET: api/<BasketController>
        [HttpGet]
        public IActionResult Get(string UserId)
        {
            var basket = _basketServices.GetOrCreatebasketForUser(UserId);
            return Ok(basket);
        }

        [HttpPost]
        public IActionResult AddItemToBasket(AddItemBasektDto request,string UserId)
        {
            var basket = _basketServices.GetOrCreatebasketForUser(UserId);
            request.BasketId = basket.Id;
            _basketServices.AddItemBasket(request);
            var bsaketData = _basketServices.GetBasket(UserId);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(Guid ItemId)
        {
            _basketServices.RemoveitemFromBasket(ItemId);
            return Ok();
        }

        [HttpPut]
        public IActionResult SetQuntity(Guid basketItemId,int quantity)
        {
            _basketServices.SetQunatities(basketItemId,quantity);
            return Ok();      
        }





    }
}
