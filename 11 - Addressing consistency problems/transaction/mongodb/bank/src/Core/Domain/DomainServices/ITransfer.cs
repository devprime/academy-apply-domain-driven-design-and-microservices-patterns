using System;
using DevPrime.Stack.Foundation;
using Domain.Aggregates.Account;

namespace Domain.DomainServices
{
    public interface ITransfer : IDevPrimeDomain
    {
        void BankTransfer(Account origin, Account destination, double value);
    }
}