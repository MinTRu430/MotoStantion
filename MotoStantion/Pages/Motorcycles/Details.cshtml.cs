using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Motorcycles
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Motorcycle Motorcycle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles
                .Include(m => m.Customer) // Включаем зависимые данные
                .FirstOrDefaultAsync(m => m.MotorcycleId == id);

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
    }
}
