namespace Domain.Aggregates.Order;
public class Mesure : ValueObject
{
    public UnitOfMesure UnitOfMesure { get; private set; }
    public double Quantity { get; private set; }
    public Mesure(string unitOfMesure, double quantity)
    {
        unitOfMesureValue = unitOfMesure;
        Quantity = quantity;
    }
    public Mesure()
    {
    }
    private string unitOfMesureValue { get; set; }
}