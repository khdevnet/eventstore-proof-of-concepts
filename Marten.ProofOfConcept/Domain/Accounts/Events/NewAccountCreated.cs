using Marten.ProofOfConcept.Events;
using System;

namespace Marten.ProofOfConcept.Domain.Accounts.Events
{
    public class NewAccountCreated : IEvent
    {
        public Guid AccountId { get; set; }

        public Guid ClientId { get; set; }

        public string Number { get; set; }
    }
}
