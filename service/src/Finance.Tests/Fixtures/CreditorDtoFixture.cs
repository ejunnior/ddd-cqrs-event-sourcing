namespace Finance.Tests.Fixtures
{
    using System;
    using AutoFixture;

    public class CreditorDtoFixture
    {
        private readonly CreditorDto _dto;

        public CreditorDtoFixture()
        {
            var fixture = new Fixture();
            _dto = fixture.Create<CreditorDto>();
        }

        public CreditorDto Build()
        {
            return _dto;
        }

        public CreditorDtoFixture WithEmail(string email)
        {
            _dto.Email = email;
            return this;
        }

        public CreditorDtoFixture WithId(Guid id)
        {
            _dto.Id = id;
            return this;
        }

        public CreditorDtoFixture WithMobilePhone(string mobilePhone)
        {
            _dto.MobilePhone = mobilePhone;
            return this;
        }

        public CreditorDtoFixture WithName(string name)
        {
            _dto.Name = name;
            return this;
        }
    }
}