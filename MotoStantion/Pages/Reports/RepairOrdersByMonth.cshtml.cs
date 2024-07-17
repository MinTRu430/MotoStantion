using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class RepairOrdersByMonthModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RepairOrdersByMonthModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RepairOrdersByMonthData> RepairOrdersByMonth { get; set; }

        public async Task OnGetAsync()
        {
            RepairOrdersByMonth = await _context.RepairOrders
                .GroupBy(ro => new { ro.OrderDate.Year, ro.OrderDate.Month })
                .Select(g => new RepairOrdersByMonthData
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Year).ThenBy(r => r.Month)
                .ToListAsync();
        }

        public class RepairOrdersByMonthData
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int OrderCount { get; set; }

            public string MonthYear => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
        }
    }
}
