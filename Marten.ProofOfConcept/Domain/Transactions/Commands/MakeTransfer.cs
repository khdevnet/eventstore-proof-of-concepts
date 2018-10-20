using System;

namespace Marten.ProofOfConcept.Domain.Accounts.Transactions.Commands
{
    public class MakeTransfer
    {
        public Guid FromAccountId { get; }
        public Guid ToAccountId { get; }
        public decimal Ammount { get; }

        public MakeTransfer(decimal ammount, Guid fromAccountId, Guid toAccountId)
        {
            Ammount = ammount;
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
        }
    }
}
