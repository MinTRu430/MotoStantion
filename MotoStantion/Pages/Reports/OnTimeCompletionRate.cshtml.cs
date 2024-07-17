using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class OnTimeCompletionRateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OnTimeCompletionRateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public double OnTimeCompletionRate { get; set; }

        public async Task OnGetAsync()
        {
            var totalOrders = await _context.RepairOrders.CountAsync();
            var onTimeOrders = await _context.RepairOrders
                .Where(ro => ro.ActualCompletionDate.HasValue && ro.EstimatedCompletionDate.HasValue &&
                             ro.ActualCompletionDate.Value.Date <= ro.EstimatedCompletionDate.Value.Date)
                .CountAsync();

            if (totalOrders > 0)
            {
                OnTimeCompletionRate = (double)onTimeOrders / totalOrders;
            }
            else
            {
                OnTimeCompletionRate = 0;
            }
        }
    }
}
