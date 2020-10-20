namespace Application.Bank.Dto
{
    using System;

    public class RegisterBankAccountDto
    {
        public string AccountNumber { get; set; }

        public decimal InitialBalanceAmount { get; set; }
    }
}