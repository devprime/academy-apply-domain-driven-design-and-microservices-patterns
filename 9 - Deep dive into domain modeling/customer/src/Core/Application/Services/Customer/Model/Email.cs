namespace Application.Services.Customer.Model;
public class Email
{
    public string Value { get; set; }
    public virtual IList<Email> ToEmailList(IList<Domain.Aggregates.Customer.Email> emailList)
    {
        var _emailList = ToApplication(emailList);
        return _emailList;
    }
    public virtual Email ToEmail(Domain.Aggregates.Customer.Email email)
    {
        var _email = ToApplication(email);
        return _email;
    }
    public virtual Domain.Aggregates.Customer.Email ToDomain()
    {
        var _email = ToDomain(this);
        return _email;
    }
    public Email()
    {
    }
    public static Application.Services.Customer.Model.Email ToApplication(Domain.Aggregates.Customer.Email email)
    {
        if (email is null)
            return new Application.Services.Customer.Model.Email();
        Application.Services.Customer.Model.Email _email = new Application.Services.Customer.Model.Email();
        _email.Value = email.Value;
        return _email;
    }
    public static List<Application.Services.Customer.Model.Email> ToApplication(IList<Domain.Aggregates.Customer.Email> emailList)
    {
        List<Application.Services.Customer.Model.Email> _emailList = new List<Application.Services.Customer.Model.Email>();
        if (emailList != null)
        {
            foreach (var email in emailList)
            {
                Application.Services.Customer.Model.Email _email = new Application.Services.Customer.Model.Email();
                _email.Value = email.Value;
                _emailList.Add(_email);
            }
        }
        return _emailList;
    }
    public static Domain.Aggregates.Customer.Email ToDomain(Application.Services.Customer.Model.Email email)
    {
        if (email is null)
            return new Domain.Aggregates.Customer.Email();
        Domain.Aggregates.Customer.Email _email = new Domain.Aggregates.Customer.Email(email.Value);
        return _email;
    }
    public static List<Domain.Aggregates.Customer.Email> ToDomain(IList<Application.Services.Customer.Model.Email> emailList)
    {
        List<Domain.Aggregates.Customer.Email> _emailList = new List<Domain.Aggregates.Customer.Email>();
        if (emailList != null)
        {
            foreach (var email in emailList)
            {
                Domain.Aggregates.Customer.Email _email = new Domain.Aggregates.Customer.Email(email.Value);
                _emailList.Add(_email);
            }
        }
        return _emailList;
    }
}