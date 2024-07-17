using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class AverageRepairTimeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AverageRepairTimeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public double AverageRepairTimeInDays { get; set; }

        public async Task OnGetAsync()
        {
            var repairTimes = await _context.RepairOrders
                .Where(ro => ro.ActualCompletionDate.HasValue)
                .Select(ro => (ro.ActualCompletionDate.Value - ro.OrderDate).TotalDays)
                .ToListAsync();

            if (repairTimes.Count > 0)
            {
                AverageRepairTimeInDays = repairTimes.Average();
            }
            else
            {
                AverageRepairTimeInDays = 0;
            }
        }
    }
}
