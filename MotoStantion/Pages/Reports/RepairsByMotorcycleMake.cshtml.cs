using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoStation.Pages.Reports
{
    public class RepairsByMotorcycleMakeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RepairsByMotorcycleMakeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RepairsByMakeData> RepairsByMake { get; set; }

        public async Task OnGetAsync()
        {
            RepairsByMake = await _context.RepairOrders
                .Include(ro => ro.Motorcycle)
                .GroupBy(ro => ro.Motorcycle.Make)
                .Select(g => new RepairsByMakeData
                {
                    Make = g.Key,
                    RepairCount = g.Count()
                })
                .ToListAsync();
        }

        public class RepairsByMakeData
        {
            public string Make { get; set; }
            public int RepairCount { get; set; }
        }
    }
}
