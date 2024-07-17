using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<SearchResult> SearchResults { get; set; }
    [BindProperty]
    public string SearchQuery { get; set; }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        SearchResults = new List<SearchResult>();

        if (!string.IsNullOrEmpty(SearchQuery))
        {
            // Поиск по клиентам
            var customers = await _context.Customers
                .Where(c => c.FirstName.Contains(SearchQuery) || c.LastName.Contains(SearchQuery))
                .Select(c => new SearchResult
                {
                    EntityName = "Customer",
                    DisplayText = $"{c.FirstName} {c.LastName}",
                    Url = Url.Page("/Customers/Details", new { id = c.CustomerId })
                }).ToListAsync();
            SearchResults.AddRange(customers);

            // Поиск по мотоциклам
            var motorcycles = await _context.Motorcycles
                .Where(m => m.Make.Contains(SearchQuery) || m.Model.Contains(SearchQuery))
                .Select(m => new SearchResult
                {
                    EntityName = "Motorcycle",
                    DisplayText = $"{m.Make} {m.Model}",
                    Url = Url.Page("/Motorcycles/Details", new { id = m.MotorcycleId })
                }).ToListAsync();
            SearchResults.AddRange(motorcycles);

            // Поиск по заказам на ремонт
            var repairOrders = await _context.RepairOrders
                .Where(ro => ro.Status.Contains(SearchQuery))
                .Select(ro => new SearchResult
                {
                    EntityName = "RepairOrder",
                    DisplayText = $"Order {ro.RepairOrderId} - {ro.Status}",
                    Url = Url.Page("/RepairOrders/Details", new { id = ro.RepairOrderId })
                }).ToListAsync();
            SearchResults.AddRange(repairOrders);

            // Поиск по запчастям
            var parts = await _context.Parts
                .Where(p => p.PartName.Contains(SearchQuery) || p.PartDescription.Contains(SearchQuery))
                .Select(p => new SearchResult
                {
                    EntityName = "Part",
                    DisplayText = p.PartName,
                    Url = Url.Page("/Parts/Details", new { id = p.PartId })
                }).ToListAsync();
            SearchResults.AddRange(parts);

            // Поиск по сотрудникам
            var employees = await _context.Employees
                .Where(e => e.FirstName.Contains(SearchQuery) || e.LastName.Contains(SearchQuery))
                .Select(e => new SearchResult
                {
                    EntityName = "Employee",
                    DisplayText = $"{e.FirstName} {e.LastName}",
                    Url = Url.Page("/Employees/Details", new { id = e.EmployeeId })
                }).ToListAsync();
            SearchResults.AddRange(employees);
        }
    }
}
