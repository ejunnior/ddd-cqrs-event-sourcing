namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Bank.Aggregates.BankAccountAggregate;
    using CategoryAggregate;
    using Core;
    using Creditor.Aggregates.CreditorAggregate;
    using CSharpFunctionalExtensions;

    public sealed class BankPosting : AggregateRoot
    {
        private DateTime? _documentDate;
        private string _documentNumber;
        private DateTime? _paymentDate;

        public BankPosting(
            Amount amount,
            DueDate dueDate,
            Guid creditor,
            Description description,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            Guid bankAccount,
            Guid category,
            Maybe<PaymentDate> paymentDate,
            BankPostingType type)
        {
            Causes(new BankPostingRegisteredEvent(
                correlationId: Id,
                amount: amount,
                dueDate: dueDate,
                creditor: creditor,
                description: description,
                documentDate: documentDate,
                documentNumber: documentNumber,
                bankAccount: bankAccount,
                category: category,
                paymentDate: paymentDate,
                type: type));
        }

        public Amount Amount { get; private set; }

        public Guid BankAccount { get; private set; }

        public Guid Category { get; private set; }

        public Guid Creditor { get; private set; }

        public Description Description { get; private set; }

        public Maybe<DocumentDate> DocumentDate
        {
            get => _documentDate == null ? null : (DocumentDate)_documentDate;
            protected set => _documentDate = value.Unwrap(documentDate => documentDate.Value, default(DateTime?));
        }

        public Maybe<DocumentNumber> DocumentNumber
        {
            get => _documentNumber == null ? null : (DocumentNumber)_documentNumber;
            protected set => _documentNumber = value.Unwrap(documentNumber => documentNumber.Value);
        }

        public DueDate DueDate { get; private set; }

        public Maybe<PaymentDate> PaymentDate
        {
            get => _paymentDate == null ? null : (PaymentDate)_paymentDate;
            protected set => _paymentDate = value.Unwrap(paymentDate => paymentDate.Value, default(DateTime?));
        }

        public BankPostingType Type { get; private set; }

        public override void Apply(IEvent @event)
        {
            When((dynamic)@event);
            Version++;
        }

        public void Edit(
            Amount amount,
            DueDate dueDate,
            Guid creditor,
            Description description,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            Guid bankAccount,
            Guid category,
            Maybe<PaymentDate> paymentDate,
            BankPostingType type)
        {
            Causes(new BankPostingChangedEvent(
                correlationId: Id,
                amount: amount,
                dueDate: dueDate,
                creditor: creditor,
                description: description,
                documentDate: documentDate,
                documentNumber: documentNumber,
                bankAccount: bankAccount,
                category: category,
                paymentDate: paymentDate,
                type: type));
        }

        private void Causes(IEvent @event)
        {
            AddDomainEvent(@event);
            Apply(@event);
        }

        private void When(BankPostingRegisteredEvent @event)
        {
            Amount = @event.Amount;
            DueDate = @event.DueDate;
            Description = @event.Description;
            DocumentDate = @event.DocumentDate;
            DocumentNumber = @event.DocumentNumber;
            BankAccount = @event.BankAccount;
            Category = @event.Category;
            PaymentDate = @event.PaymentDate;
            Creditor = @event.Creditor;
            Type = @event.Type;
        }

        private void When(BankPostingChangedEvent @event)
        {
            Amount = @event.Amount;
            DueDate = @event.DueDate;
            Description = @event.Description;
            DocumentDate = @event.DocumentDate;
            DocumentNumber = @event.DocumentNumber;
            BankAccount = @event.BankAccount;
            Category = @event.Category;
            PaymentDate = @event.PaymentDate;
            Creditor = @event.Creditor;
            Type = @event.Type;
        }
    }
}