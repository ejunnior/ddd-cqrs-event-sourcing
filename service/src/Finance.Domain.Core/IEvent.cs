namespace Finance.Domain.Core
{
    using System;

    public interface IEvent
    {
        Guid CorrelationId { get; }

        DateTime DateOccurred { get; }
    }
}