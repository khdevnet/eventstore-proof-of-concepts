using System.Collections.Generic;
using Marten.ProofOfConcept.Aggregates;

namespace Marten.ProofOfConcept.Events
{
    public interface IEventSourcedAggregate : IAggregate
    {
        Queue<IEvent> PendingEvents { get; }
    }
}