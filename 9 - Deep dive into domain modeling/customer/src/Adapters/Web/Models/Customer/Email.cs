namespace DevPrime.Web.Models.Customer;
public class Email
{
    public string Value { get; set; }
    public static Application.Services.Customer.Model.Email ToApplication(DevPrime.Web.Models.Customer.Email email)
    {
        if (email is null)
            return new Application.Services.Customer.Model.Email();
        Application.Services.Customer.Model.Email _email = new Application.Services.Customer.Model.Email();
        _email.Value = email.Value;
        return _email;
    }
    public static List<Application.Services.Customer.Model.Email> ToApplication(IList<DevPrime.Web.Models.Customer.Email> emailList)
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
    public virtual Application.Services.Customer.Model.Email ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}