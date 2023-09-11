namespace Domain.Aggregates.Order;
public enum UnitOfMesure
{
    Kilo = 1,
    Pound = 2,
    Ounce = 3,
    Unit = 4,
    Pack = 5
}
public static class UnitOfMesureParse
{
    public static UnitOfMesure GetUnitOfMesureValue(string unitOfMesureValue)
    {
        if (!String.IsNullOrWhiteSpace(unitOfMesureValue))
        {
            if (unitOfMesureValue?.ToLower() == "kilo")
                return UnitOfMesure.Kilo;
            else if (unitOfMesureValue?.ToLower() == "pound")
                return UnitOfMesure.Pound;
            else if (unitOfMesureValue?.ToLower() == "ounce")
                return UnitOfMesure.Ounce;
            else if (unitOfMesureValue?.ToLower() == "unit")
                return UnitOfMesure.Unit;
            else if (unitOfMesureValue?.ToLower() == "pack")
                return UnitOfMesure.Pack;
            else
                throw new PublicException($"{unitOfMesureValue} is an invalid UnitOfMesure, try: 'Kilo', 'Pound', 'Ounce', 'Unit', 'Pack' ");
        }
        else
        {
            throw new PublicException($"UnitOfMesure can not be null or empty ");
        }
    }
}