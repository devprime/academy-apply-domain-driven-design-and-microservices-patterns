namespace Domain.Aggregates.License;
public enum LicenseType
{
    Developer = 1,
    Free = 2,
    Enterprise = 3
}
public static class TypeParse
{
    public static LicenseType GetTypeValue(string typeValue)
    {
        if (!String.IsNullOrWhiteSpace(typeValue))
        {
            if (typeValue?.ToLower() == "developer")
                return LicenseType.Developer;
            else if (typeValue?.ToLower() == "free")
                return LicenseType.Free;
            else if (typeValue?.ToLower() == "enterprise")
                return LicenseType.Enterprise;
            else
                throw new PublicException($"{typeValue} is an invalid LicenseType, try: 'Developer', 'Free', 'Enterprise' ");
        }
        else
        {
            throw new PublicException($"LicenseType can not be null or empty ");
        }
    }
}