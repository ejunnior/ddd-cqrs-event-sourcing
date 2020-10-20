namespace Finance.Application.Treasury.Queries
{
    using Dapper;
    using Domain.Core;
    using Dtos;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetBankPostingQuery
        : IQuery<IList<GetBankPostingDto>>
    {
        public GetBankPostingQuery()
        {
        }
    }

    public class GetBankPostingQueryHandler
        : IQueryHandler<GetBankPostingQuery, IList<GetBankPostingDto>>
    {
        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        public async Task<IList<GetBankPostingDto>> HandleAsync(GetBankPostingQuery args)
        {
            var sql = @"
                    select
                          bapo.amount
                        , bapo.duedate
                        , bapo.id
                        , bapo.documentDate
                        , bapo.documentNumber
                        , bapo.paymentDate
                        , cred.name as creditor
                        , bapo.description
                        , bapo.type
                    from bankposting bapo
                         inner join creditor cred on bapo.creditorId = cred.Id
                    order by duedate";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = await connection
                    .QueryAsync<GetBankPostingDto>(sql);

                return result.ToList();
            }
        }
    }
}