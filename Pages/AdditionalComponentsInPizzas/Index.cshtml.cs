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

namespace WebApp.Pages_AdditionalComponentsInPizzas
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<AdditionalComponentInPizza> AdditionalComponentInPizza { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AdditionalComponentInPizza = await _context.AdditionalComponentsInPizzas
                .Include(a => a.AdditionalComponent)
                .Include(pizza => pizza.Pizza)
                .Include(p => p.PizzaInOrder)
                .ThenInclude(o => o!.Order)
                .ToListAsync();
        } 
    }
}
