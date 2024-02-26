namespace Domain;

public class PizzaInOrder : BaseEntity
{
    public Guid PizzaId { get; set; }
    public Pizza? Pizza { get; set; }
    
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal Price { get; set; }
    
    public ICollection<AdditionalComponentInPizza>? AdditionalComponents { get; set; }
}