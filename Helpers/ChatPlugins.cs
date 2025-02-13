using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text.Json;

namespace CursorProjects.Application
{
    public class ChatPlugins
    {
        private readonly ApplicationDbContext _context;

        public ChatPlugins(ApplicationDbContext context)
        {
            _context = context;
        }


        [KernelFunction]
        [Description("Get whole furniture inventory")]
        public string GetInventory() 
        {
            var list = _context.Products
                .AsNoTracking()
                .Select(x=> new { x.Id, x.Name, x.Description, Price = $"{x.Price}$" })
                .ToList();

            return JsonSerializer.Serialize(list);
        }

    }
}
