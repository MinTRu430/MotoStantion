using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoStation.Pages.Motorcycles
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return Page();
        }

        [BindProperty]
        public Motorcycle Motorcycle { get; set; } = new Motorcycle();

        [BindProperty]
        public IFormFile? Image { get; set; } // Свойство для загрузки изображения

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    Motorcycle.Image = memoryStream.ToArray();
                }
            }

            _context.Motorcycles.Add(Motorcycle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
