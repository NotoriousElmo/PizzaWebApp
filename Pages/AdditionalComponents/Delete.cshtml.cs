using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_AdditionalComponents
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdditionalComponent AdditionalComponent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalcomponent = await _context.AdditionalComponents.FirstOrDefaultAsync(m => m.Id == id);

            if (additionalcomponent == null)
            {
                return NotFound();
            }
            else
            {
                AdditionalComponent = additionalcomponent;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalcomponent = await _context.AdditionalComponents.FindAsync(id);
            if (additionalcomponent != null)
            {
                AdditionalComponent = additionalcomponent;
                _context.AdditionalComponents.Remove(AdditionalComponent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
