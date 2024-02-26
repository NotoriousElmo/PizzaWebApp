using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Pizza : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public string Category { get; set; } = default!;

    public ICollection<PizzaInOrder>? PizzaInOrders { get; set; }
}