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
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalcomponentinpizza = await _context.AdditionalComponentsInPizzas.FindAsync(id);
            if (additionalcomponentinpizza != null)
            {
                AdditionalComponentInPizza = additionalcomponentinpizza;
                _context.AdditionalComponentsInPizzas.Remove(AdditionalComponentInPizza);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
