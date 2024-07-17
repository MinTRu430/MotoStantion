using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.RepairOrders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public RepairOrder RepairOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairorder = await _context.RepairOrders
                .Include(ro => ro.Motorcycle)
                .ThenInclude(m => m.Customer)
                .Include(ro => ro.Employee)
                .FirstOrDefaultAsync(m => m.RepairOrderId == id);

            if (repairorder == null)
            {
                return NotFound();
            }
            else
            {
                RepairOrder = repairorder;
            }
            return Page();
        }
    }
}
