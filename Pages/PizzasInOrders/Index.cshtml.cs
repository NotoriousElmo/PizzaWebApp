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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<PizzaInOrder> PizzaInOrder { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PizzaInOrder = await _context.PizzaInOrders
                .Include(p => p.Order)
                .Include(p => p.Pizza).ToListAsync();
        }
    }
}
