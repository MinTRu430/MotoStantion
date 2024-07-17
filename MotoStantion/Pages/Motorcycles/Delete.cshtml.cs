using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Motorcycles
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Motorcycle Motorcycle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(m => m.MotorcycleId == id);

            if (motorcycle == null)
            {
                return NotFound();
            }
            else
            {
                Motorcycle = motorcycle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle != null)
            {
                Motorcycle = motorcycle;
                _context.Motorcycles.Remove(Motorcycle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
