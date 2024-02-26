using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages_AdditionalComponentsInPizzas
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }
        
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public IActionResult OnGet()
        {
            var pizzaInOrders = _context.PizzaInOrders
                .Where(x => x.OrderId.Equals(Id))
                .Include(p => p.Pizza)
                .Include(p => p.Order)
                .Select(p => new 
                { 
                    p.Id, 
                    Description = $"{p.Pizza!.Name}, {p.Order!.ClientName}" 
                });

            ViewData["PizzaInOrderId"] = new SelectList(pizzaInOrders, "Id", "Description");
            ViewData["AdditionalComponentId"] = new SelectList(_context.AdditionalComponents, "Id", "Name");
            return Page();
        }



        [BindProperty]
        public AdditionalComponentInPizza AdditionalComponentInPizza { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AdditionalComponentInPizza.Price = _context.AdditionalComponents
                .Where(x => x.Id.Equals(AdditionalComponentInPizza.AdditionalComponentId))
                .ToList()[0].Price;
            
            var pizza = _context.PizzaInOrders
                .FirstOrDefault(x => AdditionalComponentInPizza.PizzaInOrderId.Equals(x.Id));
            
            if (pizza != null)
            {
                if (pizza.AdditionalComponents != null)
                {
                    pizza.AdditionalComponents.Add(AdditionalComponentInPizza);
                }
                else
                {
                    pizza.AdditionalComponents = new List<AdditionalComponentInPizza>
                    {
                        AdditionalComponentInPizza
                    };
                }

                pizza.Price += AdditionalComponentInPizza.Price;
            }
            
            AdditionalComponentInPizza.Pizza = _context.Pizzas.FirstOrDefault(x => x.Id.Equals(pizza!.PizzaId));
            
            AdditionalComponentInPizza.AdditionalComponent =
                _context.AdditionalComponents.FirstOrDefault(x =>
                    x.Id.Equals(AdditionalComponentInPizza.AdditionalComponentId));
            
            _context.AdditionalComponentsInPizzas.Add(AdditionalComponentInPizza);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
