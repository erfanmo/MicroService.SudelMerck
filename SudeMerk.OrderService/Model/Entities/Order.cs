namespace SudeMerk.OrderService.Model.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderPlaced { get;  private set; }
        public bool OrderPaid { get; private set; }
        public ICollection<OrderLine> OrderLines { get; private set; }
        public Order(string UserId,List<OrderLine> orderLines)
        {
            this.UserId = UserId;
            this.OrderPaid = false;
            this.OrderPlaced = DateTime.Now; 
            this.OrderLines = orderLines;
        }
        public Order()
        { }
    }
}
