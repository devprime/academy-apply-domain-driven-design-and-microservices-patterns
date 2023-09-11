using System.Linq;
using System;
using DevPrime.Stack.Foundation.Exceptions;
using DevPrime.Stack.Foundation;
namespace Domain.Aggregates.Account
{
    public class Account : AggRoot
    {
        public string Number { get; private set; }
        public double Balance { get; private set; }
        public virtual void Credit(double amount)
        {
            Dp.Pipeline(Execute: () =>
            {
                Balance = Balance + amount;
                Dp.ProcessEvent(new UpdateAccount());
            });
        }
        public virtual void Debit(double amount)
        {
            Dp.Pipeline(Execute: () =>
            {
                Balance = Balance - amount;
                Dp.ProcessEvent(new UpdateAccount());
            });
        }
        public Account(Guid id, string number, double balance)
        {
            ID = id;
            Number = number;
            Balance = balance;
        }
        public Account()
        {
        }
        public virtual void Add()
        {
            Dp.Pipeline(Execute: () =>
            {
                ValidFields();
                ID = Guid.NewGuid();
                IsNew = true;
                Dp.ProcessEvent(new CreateAccount());
            });
        }
        public virtual void Update()
        {
            Dp.Pipeline(Execute: () =>
            {
                ValidFields();
                Dp.ProcessEvent(new UpdateAccount());
            });
        }
        public virtual void Delete()
        {
            Dp.Pipeline(Execute: () =>
            {
                if (ID != Guid.Empty)
                    Dp.ProcessEvent(new DeleteAccount());
            });
        }
        public virtual (List<Account> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
        {
            return Dp.Pipeline(ExecuteResult: () =>
            {
                if (offset == null && limit != null)
                {
                    throw new PublicException("Offset is required if you have limit");
                }
                else if (offset != null && limit == null)
                {
                    throw new PublicException("Limit is required if you have offset");
                }
                else if (offset != null && limit != null)
                {
                    if (offset < 1)
                        throw new PublicException("Offset must be greater than 1");
                    if (limit < 1)
                        throw new PublicException("Limit must be greater than 1");
                }
                if (string.IsNullOrWhiteSpace(ordering) && !string.IsNullOrWhiteSpace(sort))
                {
                    throw new PublicException("Ordering is required if you have sort");
                }
                else if (!string.IsNullOrWhiteSpace(ordering) && string.IsNullOrWhiteSpace(sort))
                {
                    throw new PublicException("Sort is required if you have ordering");
                }
                else if (!string.IsNullOrWhiteSpace(sort) && !string.IsNullOrWhiteSpace(ordering))
                {
                    if (sort?.ToLower() != "desc" && sort?.ToLower() != "asc")
                        throw new PublicException("Sort must be 'Asc' or 'Desc'");
                    bool orderingIsValid = false;
                    if (ordering?.ToLower() == "id")
                        orderingIsValid = true;
                    if (ordering?.ToLower() == "number")
                        orderingIsValid = true;
                    if (ordering?.ToLower() == "balance")
                        orderingIsValid = true;
                    if (!orderingIsValid)
                        throw new PublicException($"Ordering '{ordering}' is invalid try: 'ID=somevalue', 'Number=somevalue', 'Balance=somevalue',");
                }
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    bool filterIsValid = false;
                    if (filter.Contains("="))
                    {
                        if (filter.ToLower().StartsWith("id="))
                            filterIsValid = true;
                        if (filter.ToLower().StartsWith("number="))
                            filterIsValid = true;
                        if (filter.ToLower().StartsWith("balance="))
                            filterIsValid = true;
                    }
                    if (!filterIsValid)
                        throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Number', 'Balance',");
                }
                var source = Dp.ProcessEvent(new GetAccount()
                {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
                return source;
            });
        }
        private void ValidFields()
        {
            if (String.IsNullOrWhiteSpace(Number))
                Dp.Notifications.Add("Number is required");
            Dp.Notifications.ValidateAndThrow();
        }
    }
}