using System;
using Marten.Events.Projections;
using Marten.ProofOfConcept.Domain.Accounts.Events;
using Marten.ProofOfConcept.Domain.Accounts.Transactions.Events;

namespace Marten.ProofOfConcept.Views.Accounts.AccountSummary
{
    public class AccountSummaryViewProjection : ViewProjection<AccountSummaryView, Guid>
    {
        public AccountSummaryViewProjection()
        {
            ProjectEvent<NewAccountCreated>((ev) => ev.AccountId, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<NewInflowRecorded>((ev) => ev.ToAccountId, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<NewOutflowRecorded>((ev) => ev.FromAccountId, (view, @event) => view.ApplyEvent(@event));
            //ProjectEvent((IDocumentSession session, ClientCreated @event) => FindClientAccountIds(session, @event), (view, @event) => view.ApplyEvent(@event));
            //ProjectEvent((IDocumentSession session, ClientUpdated @event) => FindClientAccountIds(session, @event), (view, @event) => view.ApplyEvent(@event));
            //ProjectEvent((IDocumentSession session, ClientDeleted @event) => FindClientAccountIds(session, @event), (view, @event) => view.ApplyEvent(@event));
        }

        //private List<Guid> FindClientAccountIds(IDocumentSession session, ClientCreated @event)
        //{
        //    return FindClientAccountIds(session, @event.ClientId);
        //}

        //private List<Guid> FindClientAccountIds(IDocumentSession session, ClientUpdated @event)
        //{
        //    return FindClientAccountIds(session, @event.ClientId);
        //}

        //private List<Guid> FindClientAccountIds(IDocumentSession session, ClientDeleted @event)
        //{
        //    return FindClientAccountIds(session, @event.ClientId);
        //}

        //private List<Guid> FindClientAccountIds(IDocumentSession session, Guid clientId)
        //{
        //    return session.Query<AccountSummaryView>()
        //        .Where(a => a.ClientId == clientId)
        //        .Select(a => a.AccountId)
        //        .ToList();
        //}
    }
}