using System.ComponentModel.DataAnnotations;

namespace Domain;

public class AdditionalComponent : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public decimal Price { get; set; }
    
    public ICollection<AdditionalComponentInPizza>? ComponentInPizzas { get; set; }
}