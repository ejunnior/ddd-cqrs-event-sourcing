namespace Finance.Infrastructure.Data.Core
{
    using System;

    public class SnapshotWrapper
    {
        public DateTime Created { get; set; }

        public Object Snapshot { get; set; }

        public string StreamName { get; set; }
    }
}