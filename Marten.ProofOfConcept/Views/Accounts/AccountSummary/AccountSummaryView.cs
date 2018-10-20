using System;
using Marten.ProofOfConcept.Domain.Accounts.Events;
using Marten.ProofOfConcept.Domain.Accounts.Transactions.Events;

namespace Marten.ProofOfConcept.Views.Accounts.AccountSummary
{
    public class AccountSummaryView
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public int TransactionsCount { get; set; }
        public bool IsDeleted { get; set; }

        public void ApplyEvent(NewAccountCreated @event)
        {
            AccountId = @event.AccountId;
            Balance = 0;
            ClientId = @event.ClientId;
            Number = @event.Number;
            TransactionsCount = 0;
        }

        public void ApplyEvent(NewInflowRecorded @event)
        {
            Balance += @event.Inflow.Ammount;
        }

        //internal void ApplyEvent(ClientCreated @event)
        //{
        //    ClientName = @event.Data.Name;
        //}

        //internal void ApplyEvent(ClientUpdated @event)
        //{
        //    ClientName = @event.Data.Name;
        //}

        //internal void ApplyEvent(ClientDeleted @event)
        //{
        //    IsDeleted = false;
        //}

        public void ApplyEvent(NewOutflowRecorded @event)
        {
            Balance -= @event.Outflow.Ammount;
        }
    }
}
