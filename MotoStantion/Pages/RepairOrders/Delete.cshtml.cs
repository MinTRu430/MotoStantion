using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.RepairOrders
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RepairOrder RepairOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairorder = await _context.RepairOrders.FirstOrDefaultAsync(m => m.RepairOrderId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairorder = await _context.RepairOrders.FindAsync(id);
            if (repairorder != null)
            {
                RepairOrder = repairorder;
                _context.RepairOrders.Remove(RepairOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
