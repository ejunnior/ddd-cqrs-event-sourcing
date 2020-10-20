namespace Finance.Application.Treasury.Queries
{
    using Dapper;
    using Domain.Core;
    using Dtos;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Threading.Tasks;

    public class GetBankPostingByIdQuery
        : IQuery<GetBankPostingByIdDto>
    {
        public GetBankPostingByIdQuery(Guid bankPostingId)
        {
            BankPostingId = bankPostingId;
        }

        public Guid BankPostingId { get; }
    }

    public class GetBankPostingByIdQueryHandler
        : IQueryHandler<GetBankPostingByIdQuery, GetBankPostingByIdDto>
    {
        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        public async Task<GetBankPostingByIdDto> HandleAsync(GetBankPostingByIdQuery args)
        {
            var sql = @"
                    select
                          bapo.amount
                        , bapo.dueDate
                        , bapo.id
                        , bapo.documentDate
                        , bapo.documentNumber
                        , bapo.paymentDate
                        , cred.name as creditor
                        , bapo.description
                        , bapo.creditorId
                        , bapo.categoryid
                        , bapo.bankaccountid
                        , bapo.type
                    from bankposting bapo
                         inner join creditor cred on bapo.creditorId = cred.Id
                         inner join bankaccount baac on bapo.bankAccountId = baac.Id
                    where bapo.id = @id
                    order by duedate";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = await connection
                    .QuerySingleOrDefaultAsync<GetBankPostingByIdDto>(sql, new
                    {
                        id = args.BankPostingId
                    });

                return result;
            }
        }
    }
}