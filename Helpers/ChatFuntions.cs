using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace CursorProjects.Application
{
    public class ChatFunctions
    {
        private readonly ApplicationDbContext _context;

        public ChatFunctions(ApplicationDbContext context)
        {
            _context = context;
        }


        [KernelFunction]
        [Description("Add furniture order")]
        public string AddOrder([Description("SQL Statement for adding furniture order to table \"Orders\" with columns: \"ProductId\", \"Quantity\", \"Customer Name\" ")] string sql )
        {
            try
            { 
            _context.Database.ExecuteSqlRaw(sql);
                _context.SaveChanges();
            Console.WriteLine(sql);
                return "Order added successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
