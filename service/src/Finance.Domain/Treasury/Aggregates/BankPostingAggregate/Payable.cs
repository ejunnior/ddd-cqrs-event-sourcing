namespace Domain.Treasury.Aggregates.BankPostingAggregate
{
    using Bank.Aggregates.BankAccountAggregate;
    using CategoryAggregate;
    using Core;
    using Vendor.Aggregates.VendorAggregate;

    public class Payable : BankPosting
    {
        public Payable(
            TotalAmount amount,
            DueDate dueDate,
            Vendor vendor,
            Description description,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            BankAccount bankAccount,
            Category cateogry,
            Maybe<PaymentDate> paymentDate)
            : base(amount: amount,
                dueDate: dueDate,
                description: description,
                vendor: vendor,
                documentDate: documentDate,
                documentNumber: documentNumber,
                bankAccount: bankAccount,
                cateogry: cateogry,
                paymentDate: paymentDate)
        {
        }

        private Payable()
        {
        }
    }
}