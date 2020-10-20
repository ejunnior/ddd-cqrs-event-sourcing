namespace Finance.Tests.Fixtures
{
    using AutoFixture;

    internal class AmountDtoFixture
    {
        private readonly AmountDto _dto;

        public AmountDtoFixture()
        {
            var fixture = new Fixture();
            _dto = fixture.Create<AmountDto>();
        }

        public AmountDto Build()
        {
            return _dto;
        }

        public AmountDtoFixture WithValue(decimal amount)
        {
            _dto.Value = amount;
            return this;
        }
    }
}