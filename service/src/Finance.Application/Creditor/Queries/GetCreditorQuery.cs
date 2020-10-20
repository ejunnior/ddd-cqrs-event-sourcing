namespace Finance.Application.Creditor.Queries
{
    using Dapper;
    using Dtos;
    using Domain.Core;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetCreditorQuery
        : IQuery<IList<GetCreditorDto>>
    {
    }

    public class GetCreditorQueryHandler
        : IQueryHandler<GetCreditorQuery, IList<GetCreditorDto>>
    {
        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        public async Task<IList<GetCreditorDto>> HandleAsync(GetCreditorQuery args)
        {
            var sql = @"
                    select
                          id
                        , name
                    from creditor
                    order by name";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = await connection
                    .QueryAsync<GetCreditorDto>(sql);

                return result.ToList();
            }
        }
    }
}