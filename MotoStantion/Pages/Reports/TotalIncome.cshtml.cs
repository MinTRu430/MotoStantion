using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class TotalIncomeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TotalIncomeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TotalIncome { get; set; }

        public async Task OnGetAsync()
        {
            TotalIncome = await _context.RepairOrders.SumAsync(ro => ro.TotalCost);
        }
    }
}
