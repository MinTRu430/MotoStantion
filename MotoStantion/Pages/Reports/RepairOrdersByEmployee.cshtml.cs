using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class RepairOrdersByEmployeeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RepairOrdersByEmployeeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RepairOrdersByEmployeeData> RepairOrdersByEmployee { get; set; }

        public async Task OnGetAsync()
        {
            RepairOrdersByEmployee = await _context.RepairOrders
                .GroupBy(ro => new { ro.Employee.FirstName, ro.Employee.LastName })
                .Select(g => new RepairOrdersByEmployeeData
                {
                    EmployeeName = g.Key.FirstName + " " + g.Key.LastName,
                    OrderCount = g.Count()
                })
                .ToListAsync();
        }

        public class RepairOrdersByEmployeeData
        {
            public string EmployeeName { get; set; }
            public int OrderCount { get; set; }
        }
    }
}
