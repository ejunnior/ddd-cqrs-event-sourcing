namespace Domain.Treasury.Aggregates.BillAggregate
{
    using Bank.Aggregates.BankAccountAggregate;
    using BankPostingAggregate;
    using ChartAccountAggregate;
    using Core;

    public class Receivable : Bill
    {
        public Receivable(
            TotalAmount amount,
            DueDate dueDate,
            Description description,
            BankAccount banckAccount,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            GlAccount glAccount,
            PaymentDate paymentDate)
            : base(amount: amount,
                dueDate: dueDate,
                description: description,
                documentDate: documentDate,
                documentNumber: documentNumber,
                bankAccount: banckAccount,
                glAccount: glAccount,
                paymentDate: paymentDate)
        {
        }

        private Receivable()
        {
        }
    }
}