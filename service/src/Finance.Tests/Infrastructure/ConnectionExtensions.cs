namespace Finance.Tests.Infrastructure
{
    using Dapper;
    using Fixtures;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Threading.Tasks;

    internal static class ConnectionExtensions
    {
        public static async Task CreateBankAccount(
            this SqlConnection connection,
            BankAccountDto dto)
        {
            //var sql = @"
            //    insert into bankaccount
            //    (
            //        id,
            //        bank,
            //        accountnumber
            //    )
            //    values
            //    (
            //        @id,
            //        @bank,
            //        @accountnumber
            //    )";

            //var parameters = new
            //{
            //    id = dto.Id,
            //    bank = dto.Bank,
            //    accountnumber = dto.AccountNumber
            //};

            //await connection
            //    .ExecuteAsync(sql, parameters);
        }

        public static async Task CreateCategory(
                    this SqlConnection connection,
            CategoryDto dto)
        {
            var sql = @"
                insert into category
                (
                    id,
                    type,
                    name
                )
                values
                (
                    @id,
                    @type,
                    @name
                )";

            var parameters = new
            {
                id = dto.Id,
                type = dto.Type,
                name = dto.Name
            };

            //await connection
            //    .ExecuteAsync(sql, parameters);
        }

        public static async Task CreateCreditor(
                    this SqlConnection connection,
            CreditorDto dto)
        {
            var sql = @"
                insert into creditor
                (
                    id,
                    email,
                    mobilephone,
                    name
                )
                values
                (
                    @id,
                    @email,
                    @mobilephone,
                    @name
                )";

            var parameters = new
            {
                id = dto.Id,
                email = dto.Email,
                mobilephone = dto.MobilePhone,
                name = dto.Name
            };

            //await connection
            //    .ExecuteAsync(sql, parameters);
        }

        public static async Task<BankPostingDto> GetBankPostingByDocumentNumber(
            this SqlConnection connection,
            string documentNumber)
        {
            return null;
            //var sql = @"
            //        select
            //              amount
            //            , dueDate
            //            , id
            //            , documentDate
            //            , documentNumber
            //            , paymentDate
            //            , description
            //            , creditorId
            //            , categoryid
            //            , bankaccountid
            //            , type
            //        from bankposting
            //        where documentNumber = @documentNumber";

            //var parameters = new
            //{
            //    documentNumber = documentNumber
            //};

            //return await connection
            //    .QuerySingleAsync<BankPostingDto>(sql, parameters);
        }

        public static async Task<CategoryDto> GetCategoryById(
                    this SqlConnection connection,
            Guid id)
        {
            return null;
            //var sql = @"
            //        select
            //            id,
            //            type,
            //            name
            //        from category
            //        where id = @id";

            //var parameters = new
            //{
            //    id = id
            //};

            //return await connection
            //    .QuerySingleAsync<CategoryDto>(sql, parameters);
        }

        public static async Task<CreditorDto> GetCreditorById(
                    this SqlConnection connection,
            Guid id)
        {
            return null;
            //var sql = @"
            //        select
            //            id,
            //            email,
            //            mobilephone,
            //            name
            //        from creditor
            //        where id = @id";

            //var parameters = new
            //{
            //    id = id
            //};

            //return await connection
            //    .QuerySingleAsync<CreditorDto>(sql, parameters);
        }
    }
}