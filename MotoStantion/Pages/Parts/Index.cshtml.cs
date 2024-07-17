using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Parts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Part> Part { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchPartName { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Parts.AsQueryable();

            if (!string.IsNullOrEmpty(SearchPartName))
            {
                query = query.Where(p => p.PartName.Contains(SearchPartName));
            }

            Part = await query.ToListAsync();
        }
    }
}
