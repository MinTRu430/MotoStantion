using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class RepairOrdersByCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RepairOrdersByCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RepairOrdersByCustomerData> RepairOrdersByCustomer { get; set; }

        public async Task OnGetAsync()
        {
            RepairOrdersByCustomer = await _context.RepairOrders
                .GroupBy(ro => new { ro.Motorcycle.Customer.FirstName, ro.Motorcycle.Customer.LastName })
                .Select(g => new RepairOrdersByCustomerData
                {
                    CustomerName = g.Key.FirstName + " " + g.Key.LastName,
                    OrderCount = g.Count()
                })
                .ToListAsync();
        }

        public class RepairOrdersByCustomerData
        {
            public string CustomerName { get; set; }
            public int OrderCount { get; set; }
        }
    }
}
