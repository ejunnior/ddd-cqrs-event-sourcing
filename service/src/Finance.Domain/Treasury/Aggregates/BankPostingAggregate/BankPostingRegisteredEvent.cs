namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using Core;
    using CSharpFunctionalExtensions;
    using System;

    public class BankPostingRegisteredEvent : IEvent
    {
        private DateTime? _documentDate;
        private string _documentNumber;
        private DateTime? _paymentDate;

        public BankPostingRegisteredEvent(
            Guid correlationId,
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
            CorrelationId = correlationId;
            Amount = amount;
            DueDate = dueDate;
            Creditor = creditor;
            Description = description;
            DocumentDate = documentDate;
            DocumentNumber = documentNumber;
            BankAccount = bankAccount;
            Category = category;
            PaymentDate = paymentDate;
            Type = type;
            DateOccurred = DateTime.UtcNow;
        }

        public Amount Amount { get; }

        public Guid BankAccount { get; }

        public Guid Category { get; }

        public Guid CorrelationId { get; }

        public Guid Creditor { get; }

        public DateTime DateOccurred { get; }

        public Description Description { get; }

        public Maybe<DocumentDate> DocumentDate
        {
            get => _documentDate == null ? null : (DocumentDate)_documentDate;
            set => _documentDate = value.Unwrap(documentDate => documentDate.Value, default(DateTime?));
        }

        public Maybe<DocumentNumber> DocumentNumber
        {
            get => _documentNumber == null ? null : (DocumentNumber)_documentNumber;
            protected set => _documentNumber = value.Unwrap(documentNumber => documentNumber.Value);
        }

        public DueDate DueDate { get; protected set; }

        public Maybe<PaymentDate> PaymentDate
        {
            get => _paymentDate == null ? null : (PaymentDate)_paymentDate;
            protected set => _paymentDate = value.Unwrap(paymentDate => paymentDate.Value, default(DateTime?));
        }

        public BankPostingType Type { get; }
    }
}