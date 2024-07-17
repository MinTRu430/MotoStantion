using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoStation.Pages.RepairOrders
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["MotorcycleId"] = new SelectList(_context.Motorcycles, "MotorcycleId", "MotorcycleId");
            return Page();
        }

        [BindProperty]
        public RepairOrder RepairOrder { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RepairOrder.OrderDate = DateTime.SpecifyKind(RepairOrder.OrderDate, DateTimeKind.Utc);
            if (RepairOrder.EstimatedCompletionDate.HasValue)
            {
                RepairOrder.EstimatedCompletionDate = DateTime.SpecifyKind(RepairOrder.EstimatedCompletionDate.Value, DateTimeKind.Utc);
            }
            if (RepairOrder.ActualCompletionDate.HasValue)
            {
                RepairOrder.ActualCompletionDate = DateTime.SpecifyKind(RepairOrder.ActualCompletionDate.Value, DateTimeKind.Utc);
            }

            _context.RepairOrders.Add(RepairOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
