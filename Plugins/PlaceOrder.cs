using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace CursorProjects.Plugins
{
    public class PlaceOrder
    {
        private readonly IOrderStore _store;
        public PlaceOrder(IOrderStore context)
        {
            _store = context;
        }

        [KernelFunction]
        [Description("Add furniture order")]
        public async Task AddOrder(int productId, int quantity, string customerName)
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