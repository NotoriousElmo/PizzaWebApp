using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Orders
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;
        public Dictionary<string, decimal> OrderTotal = new();
        public Dictionary<string, string> OrderSummary = new();

        public async Task OnGet()
        {
            Order = await _context.Orders.ToListAsync();

            foreach (var order in Order)
            {
                if (!OrderTotal.ContainsKey(order.ClientName))
                {
                    OrderTotal[order.ClientName] = 0;
                    OrderSummary[order.ClientName] = "";
                }
                
                var pizzasInOrder = _context.PizzaInOrders.Where(x => x.OrderId.Equals(order.Id))
                    .Include(x => x.Pizza)
                    .ToList();

                foreach (var pizzaInOrder in pizzasInOrder)
                {
                    OrderTotal[order.ClientName] += pizzaInOrder.Price;
                    if (pizzaInOrder.Pizza != null)
                    {
                        OrderSummary[order.ClientName] += " " + pizzaInOrder.Pizza.Name;
                    }

                    var additionalItems = _context.AdditionalComponentsInPizzas
                        .Where(x => x.PizzaInOrderId.Equals(pizzaInOrder.Id))
                        .Include(a => a.AdditionalComponent)
                        .ToList();
                    if (additionalItems != null)
                    {
                        var additionalComponentCount = new Dictionary<string, int>();

                        foreach (var additional in additionalItems)
                        {
                            OrderTotal[order.ClientName] += additional.Price;
                            if (additional.AdditionalComponent != null)
                            {
                                if (!additionalComponentCount.ContainsKey(additional.AdditionalComponent.Name))
                                {
                                    additionalComponentCount[additional.AdditionalComponent.Name] = 1;
                                }
                                else
                                {
                                    additionalComponentCount[additional.AdditionalComponent.Name] += 1;
                                }
                            }
                        }

                        if (additionalComponentCount.Count >= 1)
                        {
                            OrderSummary[order.ClientName] += " + (";

                            foreach (var key in additionalComponentCount.Keys)
                            {
                                OrderSummary[order.ClientName] += additionalComponentCount[key] + " x " + key + " ";
                            }

                            OrderSummary[order.ClientName] += ")";
                        }
                        if (pizzasInOrder.FindLastIndex(x => x.Equals(pizzaInOrder)) < pizzasInOrder.Count - 1)
                        {
                            OrderSummary[order.ClientName] += ", ";
                        }
                    }
                }
                order.Price = OrderTotal[order.ClientName];
                await _context.SaveChangesAsync();
            }
        }
    }
}
