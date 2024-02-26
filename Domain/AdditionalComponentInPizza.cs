namespace Domain;

public class AdditionalComponentInPizza : BaseEntity
{
    public Guid PizzaInOrderId { get; set; }
    public Guid AdditionalComponentId { get; set; }
    
    public Pizza? Pizza { get; set; }
    public PizzaInOrder? PizzaInOrder { get; set; }
    public AdditionalComponent? AdditionalComponent { get; set; }

    public decimal Price { get; set; }
}