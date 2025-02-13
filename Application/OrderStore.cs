namespace CursorProjects
{
    public class OrderStore : IOrderStore
    {
        private List<AIGeneratedOrder> _orders = new List<AIGeneratedOrder>();

       public async Task AddOrder(AIGeneratedOrder order)
       {
          order.id = _orders.Count + 1;
          _orders.Add(order);
       }

        public async Task<IEnumerable<AIGeneratedOrder>> GetOrders()
        {
            return _orders;
        }

        public async Task Clear()
        {
            _orders.Clear();
        }

        public bool IsEmpty()
        {
            return _orders.Count == 0;
        }
    }

    public interface IOrderStore
    {
        Task AddOrder(AIGeneratedOrder order);
        Task<IEnumerable<AIGeneratedOrder>> GetOrders();
        Task Clear();
        bool IsEmpty();
    }

    public class AIGeneratedOrder
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public required string CustomerName { get; set; }
    }
}
