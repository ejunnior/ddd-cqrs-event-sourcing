namespace Finance.Application.Bank.Dto
{
    using System;

    public class GetBankAccountDto
    {
        public string AccountNumber { get; set; }

        public Guid Id { get; set; }
    }
}