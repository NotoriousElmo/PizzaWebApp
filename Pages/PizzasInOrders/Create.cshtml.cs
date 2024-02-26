using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_PizzasInOrders
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "ClientName");
        ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PizzaInOrder PizzaInOrder { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public Guid OrderId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            PizzaInOrder.Pizza = _context.Pizzas.FirstOrDefault(pizza => pizza.Id.Equals(PizzaInOrder.PizzaId));
            PizzaInOrder.Price = _context.Pizzas.FirstOrDefault(pizza => pizza.Id.Equals(PizzaInOrder.PizzaId))!.Price;
            PizzaInOrder.Order = _context.Orders.FirstOrDefault(x => x.Id.Equals(PizzaInOrder.OrderId));
            PizzaInOrder.OrderId = OrderId;
            PizzaInOrder.Order = _context.Orders.FirstOrDefault(x => x.Id.Equals(OrderId));
            
            var order = _context.Orders.FirstOrDefault(x => x.Id.Equals(PizzaInOrder.OrderId));
            if (order != null)
            {
                if (order.PizzasInOrder != null)
                {
                    order.PizzasInOrder.Add(PizzaInOrder);
                }
                else
                {
                    order.PizzasInOrder = new List<PizzaInOrder>
                    {
                        PizzaInOrder
                    };
                }
                order.Price += PizzaInOrder.Price;
            }
            
            _context.PizzaInOrders.Add(PizzaInOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Orders/Index");
        }
    }
}
