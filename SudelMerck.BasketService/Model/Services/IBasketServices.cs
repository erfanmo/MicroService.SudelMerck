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
