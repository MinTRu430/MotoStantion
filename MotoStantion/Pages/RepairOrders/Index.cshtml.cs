using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.RepairOrders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RepairOrder> RepairOrder { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchOrderDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchOrderDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchEstimatedCompletionDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchEstimatedCompletionDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchActualCompletionDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchActualCompletionDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? SearchTotalCost { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchMotorcycleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchEmployeeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SortDescending { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.RepairOrders
                .Include(r => r.Employee)
                .Include(r => r.Motorcycle)
                .ThenInclude(m => m.Customer)
                .AsQueryable();

            if (SearchOrderDateFrom.HasValue && SearchOrderDateTo.HasValue)
            {
                query = query.Where(r => r.OrderDate.Date >= SearchOrderDateFrom.Value.Date && r.OrderDate.Date <= SearchOrderDateTo.Value.Date);
            }

            if (SearchEstimatedCompletionDateFrom.HasValue && SearchEstimatedCompletionDateTo.HasValue)
            {
                query = query.Where(r => r.EstimatedCompletionDate.HasValue && r.EstimatedCompletionDate.Value.Date >= SearchEstimatedCompletionDateFrom.Value.Date && r.EstimatedCompletionDate.Value.Date <= SearchEstimatedCompletionDateTo.Value.Date);
            }

            if (SearchActualCompletionDateFrom.HasValue && SearchActualCompletionDateTo.HasValue)
            {
                query = query.Where(r => r.ActualCompletionDate.HasValue && r.ActualCompletionDate.Value.Date >= SearchActualCompletionDateFrom.Value.Date && r.ActualCompletionDate.Value.Date <= SearchActualCompletionDateTo.Value.Date);
            }

            if (SearchMotorcycleId.HasValue)
            {
                query = query.Where(r => r.MotorcycleId == SearchMotorcycleId.Value);
            }

            if (SearchEmployeeId.HasValue)
            {
                query = query.Where(r => r.EmployeeId == SearchEmployeeId.Value);
            }

            // Добавляем сортировку
            query = SortField switch
            {
                "OrderDate" => SortDescending ? query.OrderByDescending(r => r.OrderDate) : query.OrderBy(r => r.OrderDate),
                "EstimatedCompletionDate" => SortDescending ? query.OrderByDescending(r => r.EstimatedCompletionDate) : query.OrderBy(r => r.EstimatedCompletionDate),
                "ActualCompletionDate" => SortDescending ? query.OrderByDescending(r => r.ActualCompletionDate) : query.OrderBy(r => r.ActualCompletionDate),
                "Status" => SortDescending ? query.OrderByDescending(r => r.Status) : query.OrderBy(r => r.Status),
                "TotalCost" => SortDescending ? query.OrderByDescending(r => r.TotalCost) : query.OrderBy(r => r.TotalCost),
                _ => query.OrderBy(r => r.OrderDate), // Сортировка по умолчанию
            };

            RepairOrder = await query.ToListAsync();
        }
    }
}
