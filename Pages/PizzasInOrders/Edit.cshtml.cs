using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_PizzasInOrders
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PizzaInOrder PizzaInOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzainorder =  await _context.PizzaInOrders.FirstOrDefaultAsync(m => m.Id == id);
            if (pizzainorder == null)
            {
                return NotFound();
            }
            PizzaInOrder = pizzainorder;
           ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "ClientName");
           ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Category");
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

            _context.Attach(PizzaInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaInOrderExists(PizzaInOrder.Id))
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

        private bool PizzaInOrderExists(Guid id)
        {
            return _context.PizzaInOrders.Any(e => e.Id == id);
        }
    }
}
