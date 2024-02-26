using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages_PizzasInOrders
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public PizzaInOrder PizzaInOrder { get; set; } = default!;
        [BindProperty(SupportsGet = true)] public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var pizzasInOrder = _context.PizzaInOrders
                .Where(x => x.OrderId.Equals(Id))
                .Include(p => p.Pizza)
                .Include(p => p.AdditionalComponents)
                .Select(p => new
                {
                    p.Id,
                    Description = $"{p.Pizza!.Name}, ({string.Join(", ", p.AdditionalComponents!
                        .Select(x => x.AdditionalComponent!.Name))})"
                });

            ViewData["PizzaId"] = new SelectList(pizzasInOrder, "Id", "Description");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var pizzaInOrder = await _context.PizzaInOrders.FindAsync(PizzaInOrder.Id);
            if (pizzaInOrder != null)
            {
                _context.PizzaInOrders.Remove(pizzaInOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Orders/Index");
        }
    }
}
