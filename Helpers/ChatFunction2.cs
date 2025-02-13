using CursorProjects.Application;
using DataAccess;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text.Json;

namespace CursorProjects.Helpers
{
    public class ChatFunction2
    {
        private readonly IOrderStore _store;

        public ChatFunction2(IOrderStore store)
        {
            _store = store;
        }

        [KernelFunction]
        [Description("Add furniture order")]
        public async Task PlaceOrder(int productId, int quantity, string customerName)
        {
            var order = new AIGeneratedOrder
            {
                ProductId = productId,
                Quantity = quantity,
                CustomerName = customerName
            };

            await _store.AddOrder(order);
        }
    }
}
