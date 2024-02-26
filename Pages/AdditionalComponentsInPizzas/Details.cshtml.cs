using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_AdditionalComponentsInPizzas
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public AdditionalComponentInPizza AdditionalComponentInPizza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalcomponentinpizza = await _context.AdditionalComponentsInPizzas.FirstOrDefaultAsync(m => m.Id == id);
            if (additionalcomponentinpizza == null)
            {
                return NotFound();
            }
            else
            {
                AdditionalComponentInPizza = additionalcomponentinpizza;
            }
            return Page();
        }
    }
}
