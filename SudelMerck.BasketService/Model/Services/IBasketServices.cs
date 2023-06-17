using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SudelMerck.BasketService.Infrastracture.Context;
using SudelMerck.BasketService.Model.Entities;

namespace SudelMerck.BasketService.Model.Services
{
    public interface IBasketServices
    {
        BasketDto   GetOrCreatebasketForUser(string userId);
        BasketDto GetBasket(string userId);
        void AddItemBasket(AddItemBasektDto item);
        void RemoveitemFromBasket(Guid ItemId);
        void SetQunatities(Guid ItemId, int quantity);
        void TransferBasket(string anonymousId, string UserId);


    }

    public class BasketService : IBasketServices
    {
        private readonly BasketDataBaseContext _Context;
        private readonly IMapper _mapper;
        public BasketService(BasketDataBaseContext basketDataBaseContext, IMapper mapper)
        {
            _Context = basketDataBaseContext;
            _mapper = mapper;
        }

        public BasketDto GetBasket(string userId)
        {
            var basket = _Context.Baskets
                .Include(p => p.Items)
                .SingleOrDefault(p => p.UserId == userId);
            if (basket == null)
                return null;
            return new BasketDto
            {
                UserId = userId,
                Id = basket.Id,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
            };
        }

        public BasketDto GetOrCreatebasketForUser(string userId)
        {
            var basket = _Context.Baskets.SingleOrDefault(p => p.UserId == userId);

            if (basket == null)
            {
                return CreateBasketForUser(userId);
            }
            return new BasketDto
            {
                UserId = basket.UserId,
                Id = basket.Id,
                Items = (List<BasketItemDto>)basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ImageUrl = item.ImageUrl,
                }).ToList(),
            };
        }

        private BasketDto CreateBasketForUser(string UserId)
        {
            Basket basket = new Basket(UserId);
            _Context.Baskets.Add(basket);
            _Context.SaveChanges();
            return new BasketDto
            {
                UserId = basket.UserId,
                Id = basket.Id
            };
        }

        public void AddItemBasket(AddItemBasektDto item)
        {
            var basket = _Context.Baskets.FirstOrDefault(p => p.Id == item.BasketId);
            if (basket == null)
                throw new Exception("Basket Not Found ...!!!");

            var basketItem = _mapper.Map<BasketItem>(item);
            basket.Items.Add(basketItem);
            _Context.SaveChanges();
        }

        public void RemoveitemFromBasket(Guid ItemId)
        {
            var item = _Context.Items.SingleOrDefault(p => p.Id == ItemId);
            if (item == null)
                throw new Exception("There is no Item in Basket ...!");
            _Context.Items.Remove(item);
            _Context.SaveChanges();
        }

        public void SetQunatities(Guid ItemId, int quantity)
        {
            var item = _Context.Items.SingleOrDefault(p => p.Id == ItemId);
            item.SetQuntity(quantity);
            _Context.SaveChanges();


        }

        public void TransferBasket(string anonymousId, string UserId)
        {
            var anonymousBasket = _Context.Baskets.Include(p => p.Items)
                .SingleOrDefault(p=> p.UserId == anonymousId);

            if (anonymousBasket == null) return;
            var userBasket = _Context.Baskets.SingleOrDefault(p => p.UserId == UserId);
            if(userBasket == null)
            {
                userBasket = new Basket(UserId);
                _Context.Baskets.Add(userBasket);
                foreach (var item in anonymousBasket.Items)
                {
                    userBasket.Items.Add(new BasketItem
                    {
                        ImageUrl = item.ImageUrl,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        UnitPrice=item.UnitPrice,
                        Quantity = item.Quantity,

                    });
                }
                _Context.Baskets.Remove(anonymousBasket);
                _Context.SaveChanges();
            }

        }
    }

    public class BasketDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int Total()
        {
            if (Items.Count > 0)
            {
                int total = Items.Sum(p => p.Quantity * p.UnitPrice);
                return total;
            }
            return 0;
        }
    }

    public class BasketItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }

    public class AddItemBasektDto
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
