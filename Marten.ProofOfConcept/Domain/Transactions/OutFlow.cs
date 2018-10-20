using System;

namespace Marten.ProofOfConcept.Domain.Accounts.Transactions
{
    public class Outflow : ITransaction
    {
        public decimal Ammount { get; }

        public DateTime Timestamp { get; }

        public Outflow(decimal ammount, DateTime timestamp)
        {
            Ammount = ammount;
            Timestamp = timestamp;
        }
    }
}
