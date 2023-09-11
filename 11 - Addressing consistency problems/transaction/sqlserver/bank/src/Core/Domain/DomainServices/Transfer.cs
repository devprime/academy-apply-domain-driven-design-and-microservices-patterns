using System;
using DevPrime.Stack.Foundation;
using Domain.Aggregates.Account;

namespace Domain.DomainServices
{
    public class Transfer : DomainService, ITransfer
    {
        public Transfer(IDpDomain dp):base(dp){}
        public void BankTransfer(Account origin, Account destination, double value)
        {
            Dp.Pipeline(ExecuteTransaction: () => {
                origin.Debit(value);
                //throw new Exception("error");
                destination.Credit(value);
            });

        }
    }
}