namespace Domain.Aggregates.Customer;
public class Email : ValueObject
{
    public string Value { get; private set; }
    public bool Validate()
    {
        var email = Value;
        bool result = Dp.Pipeline(ExecuteResult: () =>
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            string[] parts = email.Split('@');
            if (parts.Length != 2)
            {
                return false;
            }
            string localPart = parts[0];
            string domainPart = parts[1];
            if (string.IsNullOrWhiteSpace(localPart) || string.IsNullOrWhiteSpace(domainPart))
            {
                return false;
            }
            foreach (char c in localPart)
            {
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_' && c != '-')
                {
                    return false;
                }
            }
            if (domainPart.Length < 2 || !domainPart.Contains(".") || domainPart.Split(".").Length != 2 || domainPart.EndsWith(".") || domainPart.StartsWith("."))
            {
                return false;
            }
            return true;
        });
        return result;
    }
    public Email(string value)
    {
        Value = value;
    }
    public Email()
    {
    }
}