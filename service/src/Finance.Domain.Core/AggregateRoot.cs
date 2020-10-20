namespace Finance.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AggregateRoot : Entity<Guid>
    {
        private readonly IList<IEvent> _domainEvents;

        protected AggregateRoot()
            : base(Guid.NewGuid())
        {
            _domainEvents = new List<IEvent>();
        }

        public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.ToList();

        public int Version { get; protected set; }

        public abstract void Apply(IEvent changes);

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected virtual void AddDomainEvent(IEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}