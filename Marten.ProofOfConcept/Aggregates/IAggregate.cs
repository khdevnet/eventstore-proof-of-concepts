using System;

namespace Marten.ProofOfConcept.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
