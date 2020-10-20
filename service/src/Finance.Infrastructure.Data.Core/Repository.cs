namespace Finance.Infrastructure.Data.Core
{
    using Domain.Core;
    using Raven.Client.Documents.Session;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Raven.Client.Documents;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                          throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AddAsync(TEntity item)
        {
            if (item != null)
            {
                var streamName = StreamNameFor(item.Id);
                await GetSet().StoreAsync(new EventStream(streamName));
                await AppendEventsToStream(streamName, item.DomainEvents);
            }
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var streamName = StreamNameFor(id);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            var snapshot = await GetLatestSnapshotAsync<TEntity>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var stream = await GetStreamAsync(streamName, fromEventNumber, toEventNumber);

            TEntity entity = null;

            if (snapshot != null)
            {
                //entity.Version = snapshot.Version;
                //InitialVersion= snapshot.Version;
                //payAsYouGoAccount = new PayAsYouGoAccount(snapshot);
            }
            else
            {
                //entity = new TEntity();
                //payAsYouGoAccount = new PayAsYouGoAccount();
            }

            foreach (var @event in stream)
            {
                entity.Apply(@event);
            }

            return entity;
        }

        private static void CheckForConcurrencyError(int? expectedVersion, EventStream stream)
        {
            var lastUpdatedVersion = stream.Version;
            if (lastUpdatedVersion != expectedVersion)
            {
                var error = $"Expected: {expectedVersion}. Found: {lastUpdatedVersion}";
                //throw new OptimsticConcurrencyException(error);
            }
        }

        private async Task AppendEventsToStream(string streamName, IEnumerable<IEvent> domainEvents, int? expectedVersion = null)
        {
            var stream = await GetSet().LoadAsync<EventStream>(streamName);

            if (expectedVersion != null)
            {
                CheckForConcurrencyError(expectedVersion, stream);
            }

            foreach (var @event in domainEvents)
            {
                await GetSet().StoreAsync(stream.RegisterEvent(@event));
            }
        }

        private async Task<TEntity> GetLatestSnapshotAsync<TEntity>(string streamName)
        {
            var latestSnapshot = await GetSet().Query<SnapshotWrapper>()
                .Where(x => x.StreamName == streamName)
                .OrderByDescending(x => x.Created)
                .FirstOrDefaultAsync();

            return (TEntity)latestSnapshot?.Snapshot;
        }

        private IAsyncDocumentSession GetSet()
        {
            return _unitOfWork.CreateSet();
        }

        private async Task<IEnumerable<IEvent>> GetStreamAsync(string streamName, int fromVersion, int toVersion)
        {
            // Get events from a specific version
            var eventWrappers = await (from stream in GetSet().Query<EventWrapper>()
                                       where stream.EventStreamId.Equals(streamName)
                                             && stream.EventNumber <= toVersion
                                             && stream.EventNumber >= fromVersion
                                       orderby stream.EventNumber
                                       select stream).ToListAsync();

            if (!eventWrappers.Any()) return null;

            var events = new List<IEvent>();

            foreach (var @event in eventWrappers)
            {
                events.Add(@event.Event);
            }

            return events;
        }

        private string StreamNameFor(Guid id)
        {
            return $"{typeof(TEntity).Name}-{id}";
        }
    }
}