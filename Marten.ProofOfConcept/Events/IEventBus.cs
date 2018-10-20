using System.Threading.Tasks;
using Marten.ProofOfConcept.Events;

namespace Domain.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}