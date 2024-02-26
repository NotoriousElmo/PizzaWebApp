using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private DAL.AppDbContext _context;

    public IndexModel(DAL.AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)] 
    public string? Search { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public bool InPizzaName { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public bool InDescription { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public bool InCategory { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public bool InAdditions { get; set; } 
    public IList<Domain.Pizza> Pizzas { get;set; } = default!;
    

    public async Task<IActionResult> OnGet()
    {
        Pizzas = await _context.Pizzas.ToListAsync();
        Console.WriteLine(Pizzas);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var query = _context.Pizzas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            if (InPizzaName)
            {
                query = query.Where(x => x.Name.ToLower().Contains(Search!.ToLower())).AsQueryable();
            }

            if (InCategory)
            {
                query = query.Where(x => x.Category.ToLower().Contains(Search.ToLower()));
            }

            if (InDescription)
            {
                query = query.Where(x => x.Description.ToLower().Contains(Search.ToLower()));
            }

            if (InAdditions)
            {
                query = query.Where(pizza => 
                    pizza.PizzaInOrders != null && pizza.PizzaInOrders.Any(pizzaInOrder => 
                        pizzaInOrder.AdditionalComponents != null && 
                        pizzaInOrder.AdditionalComponents.Any(component => 
                            component.AdditionalComponent != null &&
                            component.AdditionalComponent.Name.ToLower().Contains(Search.ToLower())
                        )
                    )
                ).AsQueryable();
            }
        }



        Pizzas = query.ToList();
        
        return Page();
    }
}