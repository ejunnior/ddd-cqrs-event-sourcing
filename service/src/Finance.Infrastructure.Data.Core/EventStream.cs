namespace Finance.Infrastructure.Data.Core
{
    using Domain.Core;

    public class EventStream
    {
        public EventStream(string id)
        {
            Id = id;
            Version = 0;
        }

        private EventStream()
        {
        }

        public string Id { get; }

        public int Version { get; private set; }

        public EventWrapper RegisterEvent(IEvent @event)
        {
            Version++;

            return new EventWrapper(@event, Version, Id);
        }
    }
}