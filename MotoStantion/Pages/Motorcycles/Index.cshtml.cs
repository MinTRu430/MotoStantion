using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.Motorcycles
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Motorcycle> Motorcycle { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchMake { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchYear { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchVin { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchCustomerId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? SearchHasImage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SortDescending { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Motorcycles
                .Include(m => m.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchMake))
            {
                query = query.Where(m => m.Make.Contains(SearchMake));
            }

            if (!string.IsNullOrEmpty(SearchModel))
            {
                query = query.Where(m => m.Model.Contains(SearchModel));
            }

            if (SearchYear.HasValue)
            {
                query = query.Where(m => m.Year == SearchYear.Value);
            }

            if (!string.IsNullOrEmpty(SearchVin))
            {
                query = query.Where(m => m.Vin.Contains(SearchVin));
            }

            if (SearchCustomerId.HasValue)
            {
                query = query.Where(m => m.CustomerId == SearchCustomerId.Value);
            }

            if (SearchHasImage.HasValue)
            {
                query = query.Where(m => m.Image != null == SearchHasImage.Value);
            }

            // Добавляем сортировку
            query = SortField switch
            {
                "Make" => SortDescending ? query.OrderByDescending(m => m.Make) : query.OrderBy(m => m.Make),
                "Model" => SortDescending ? query.OrderByDescending(m => m.Model) : query.OrderBy(m => m.Model),
                "Year" => SortDescending ? query.OrderByDescending(m => m.Year) : query.OrderBy(m => m.Year),
                "Vin" => SortDescending ? query.OrderByDescending(m => m.Vin) : query.OrderBy(m => m.Vin),
                _ => query.OrderBy(m => m.Make), // Сортировка по умолчанию
            };

            Motorcycle = await query.ToListAsync();
        }
    }
}
