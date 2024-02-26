using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Order : BaseEntity
{
    public DateTime CreatedAt = DateTime.Now;

    public decimal Price { get; set; }

    [MaxLength(128)]
    public string ClientName { get; set; } = default!;

    public ICollection<PizzaInOrder>? PizzasInOrder { get; set; }
}