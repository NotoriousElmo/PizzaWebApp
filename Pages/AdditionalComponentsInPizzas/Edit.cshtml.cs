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

namespace WebApp.Pages_AdditionalComponentsInPizzas
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
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

            var additionalcomponentinpizza =  await _context.AdditionalComponentsInPizzas.FirstOrDefaultAsync(m => m.Id == id);
            if (additionalcomponentinpizza == null)
            {
                return NotFound();
            }
            AdditionalComponentInPizza = additionalcomponentinpizza;
           ViewData["AdditionalComponentId"] = new SelectList(_context.AdditionalComponents, "Id", "Name");
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

            _context.Attach(AdditionalComponentInPizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalComponentInPizzaExists(AdditionalComponentInPizza.Id))
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

        private bool AdditionalComponentInPizzaExists(Guid id)
        {
            return _context.AdditionalComponentsInPizzas.Any(e => e.Id == id);
        }
    }
}
