namespace Finance.Infrastructure.Data.Core
{
    using Domain.Core;

    public class EventWrapper
    {
        public EventWrapper(
            IEvent @event,
            int eventNumber,
            string streamStateId)
        {
            Event = @event;
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Id = $"{streamStateId}-{EventNumber}";
        }

        public IEvent Event { get; }

        public int EventNumber { get; }

        public string EventStreamId { get; }

        public string Id { get; }
    }
}