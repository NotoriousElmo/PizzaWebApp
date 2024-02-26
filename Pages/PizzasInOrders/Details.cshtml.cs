using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_PizzasInOrders
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public PizzaInOrder PizzaInOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzainorder = await _context.PizzaInOrders.FirstOrDefaultAsync(m => m.Id == id);
            if (pizzainorder == null)
            {
                return NotFound();
            }
            else
            {
                PizzaInOrder = pizzainorder;
            }
            return Page();
        }
    }
}
