using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class AverageRepairCostModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AverageRepairCostModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal AverageCost { get; set; }

        public async Task OnGetAsync()
        {
            AverageCost = await _context.RepairOrders.AverageAsync(ro => ro.TotalCost);
        }
    }
}
