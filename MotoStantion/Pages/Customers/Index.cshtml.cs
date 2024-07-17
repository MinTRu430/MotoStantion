using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchFirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchLastName { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Customers
                .Include(c => c.Motorcycles) // Включаем зависимые данные
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchFirstName))
            {
                query = query.Where(c => c.FirstName.Contains(SearchFirstName));
            }

            if (!string.IsNullOrEmpty(SearchLastName))
            {
                query = query.Where(c => c.LastName.Contains(SearchLastName));
            }

            Customer = await query.ToListAsync();
        }
    }
}
