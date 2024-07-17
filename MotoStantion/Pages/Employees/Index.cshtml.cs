using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchFirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchLastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPosition { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(SearchFirstName))
            {
                query = query.Where(e => e.FirstName.Contains(SearchFirstName));
            }

            if (!string.IsNullOrEmpty(SearchLastName))
            {
                query = query.Where(e => e.LastName.Contains(SearchLastName));
            }

            if (!string.IsNullOrEmpty(SearchPosition))
            {
                query = query.Where(e => e.Position.Contains(SearchPosition));
            }

            Employee = await query.ToListAsync();
        }
    }
}
