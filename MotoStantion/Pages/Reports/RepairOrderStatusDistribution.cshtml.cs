using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class RepairOrderStatusDistributionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RepairOrderStatusDistributionModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StatusDistributionData> StatusDistribution { get; set; }

        public async Task OnGetAsync()
        {
            StatusDistribution = await _context.RepairOrders
                .GroupBy(ro => ro.Status)
                .Select(g => new StatusDistributionData
                {
                    Status = g.Key,
                    OrderCount = g.Count()
                })
                .ToListAsync();
        }

        public class StatusDistributionData
        {
            public string Status { get; set; }
            public int OrderCount { get; set; }
        }
    }
}
