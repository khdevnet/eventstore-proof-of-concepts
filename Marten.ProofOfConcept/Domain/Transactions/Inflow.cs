using System;

namespace Marten.ProofOfConcept.Domain.Accounts.Transactions
{
    public class Inflow : ITransaction
    {
        public decimal Ammount { get;  }

        public DateTime Timestamp { get; }

        public Inflow(decimal ammount, DateTime timestamp)
        {
            Ammount = ammount;
            Timestamp = timestamp;
        }
    }
}
