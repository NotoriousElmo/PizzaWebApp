using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Statistics : PageModel
{
    private readonly DAL.AppDbContext _context;

    public Statistics(DAL.AppDbContext context)
    {
        _context = context;
    }

    public decimal TotalSales { get; set; }
    public int TotalPizzasSold { get; set; }
    public int TotalAddsSold { get; set; }
    public string MostPopularPizza { get; set; } = default!;
    public string MostPopularAdd { get; set; } = default!;

    public void OnGet()
    {
        TotalSales = _context.Orders.Select(x => x.Price).ToList().Sum();
        TotalPizzasSold = _context.PizzaInOrders.Count();
        TotalAddsSold = _context.AdditionalComponentsInPizzas.Count();
        MostPopularPizza = GetMostPopular(_context.PizzaInOrders
            .Select(x => x.Pizza!.Name)
            .ToList());
        MostPopularAdd = GetMostPopular(_context.AdditionalComponentsInPizzas
            .Select(x => x.AdditionalComponent!.Name)
            .ToList());
    }

    private string GetMostPopular(List<string> values)
    {
        var counts = new Dictionary<string, int>();
        foreach (var value in values)
        {
            if (!counts.ContainsKey(value))
            {
                counts[value] = 0;
            }

            counts[value] += 1;
        }

        var sortedCounts = counts.OrderByDescending(x => x.Value);
        
        var highestValue = sortedCounts.First().Value;

        var mostPopular = sortedCounts
            .Where(x => x.Value == highestValue)
            .Select(x => x.Key)
            .ToList();

        var stringRep = "";

        stringRep += mostPopular[0];
        mostPopular.RemoveAt(0);

        foreach (var key in mostPopular)
        {
            stringRep += ", " + key;
        }

        return stringRep;
    }
}