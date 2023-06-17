namespace SudelMerck.BasketService.Model.Entities
{
    public class Basket
    {
        public Basket(string userId)
        {
            this.UserId = userId;
        }
        public Guid Id { get; set; }
        public string UserId { get; private set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }

}
