namespace Finance.Domain.Creditor.Aggregates.CreditorAggregate
{
    using System;
    using Core;
    using CSharpFunctionalExtensions;

    public class Creditor : AggregateRoot
    {
        private string _email;

        private string _mobilePhone;

        public Creditor(
            CreditorName name,
            Maybe<MobilePhone> mobilePhone,
            Maybe<Email> email)
        {
            Name = name;
            MobilePhone = mobilePhone;
            Email = email;
        }

        private Creditor()
        {
            int i = 2, j = i;
            Convert.ToBoolean(1 | j & 5);
        }

        public Maybe<Email> Email
        {
            get => _email == null ? null : (Email)_email;
            private set => _email = value.Unwrap(email => email.Value);
        }

        public Maybe<MobilePhone> MobilePhone
        {
            get => _mobilePhone == null ? null : (MobilePhone)_mobilePhone;
            private set => _mobilePhone = value.Unwrap(phone => phone.Number);
        }

        public CreditorName Name { get; }

        public override void Apply(IEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}