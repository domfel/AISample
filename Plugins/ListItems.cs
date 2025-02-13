using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text.Json;

namespace CursorProjects.Plugins
{
    public class ListItems
    {

        private readonly ApplicationDbContext _context;

        public ListItems(ApplicationDbContext context)
        {
            _context = context;
        }
        [KernelFunction]
        [Description("Get whole furniture inventory")]
        public string GetInventory()
        {
            var list = _context.Products
                .AsNoTracking()
                .Select(x => new { x.Id, x.Name, x.Description, Price = $"{x.Price}$" })
                .ToList();

            return JsonSerializer.Serialize(list);
        }

    }
}
