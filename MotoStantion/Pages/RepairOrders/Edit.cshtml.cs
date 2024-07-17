using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MotoStation.Pages.RepairOrders
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RepairOrder RepairOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairorder =  await _context.RepairOrders.FirstOrDefaultAsync(m => m.RepairOrderId == id);
            if (repairorder == null)
            {
                return NotFound();
            }
            RepairOrder = repairorder;
           ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
           ViewData["MotorcycleId"] = new SelectList(_context.Motorcycles, "MotorcycleId", "MotorcycleId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RepairOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairOrderExists(RepairOrder.RepairOrderId))
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

        private bool RepairOrderExists(int id)
        {
            return _context.RepairOrders.Any(e => e.RepairOrderId == id);
        }
    }
}
