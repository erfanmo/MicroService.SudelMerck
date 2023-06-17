namespace SudelMerck.BasketService.Model.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public void SetQuntity(int quantity)
        {
            Quantity = quantity;
        }
        public Guid BasketId { get; set; }
        public Basket basket { get; set; }
    }
}
