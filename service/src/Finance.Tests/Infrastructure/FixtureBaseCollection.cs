namespace Finance.Tests.Infrastructure
{
    using Xunit;

    [CollectionDefinition(Name)]
    public class FixtureBaseCollection : ICollectionFixture<FixtureBase>
    {
        public const string Name = nameof(FixtureBase);
    }
}