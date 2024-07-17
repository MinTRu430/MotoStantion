using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Motorcycles
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Motorcycle Motorcycle { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; } // Свойство для загрузки изображения

        [BindProperty]
        public bool RemoveImage { get; set; } // Свойство для удаления изображения

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
            Motorcycle = motorcycle;
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve the existing motorcycle data
            var existingMotorcycle = await _context.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.MotorcycleId == Motorcycle.MotorcycleId);

            if (existingMotorcycle == null)
            {
                return NotFound();
            }

            if (RemoveImage)
            {
                Motorcycle.Image = null;
            }
            else if (Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    Motorcycle.Image = memoryStream.ToArray();
                }
            }
            else
            {
                // Retain the existing image if neither a new image is uploaded nor the remove checkbox is checked
                Motorcycle.Image = existingMotorcycle.Image;
            }

            _context.Attach(Motorcycle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleExists(Motorcycle.MotorcycleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MotorcycleExists(int id)
        {
            return _context.Motorcycles.Any(e => e.MotorcycleId == id);
        }
    }
}
