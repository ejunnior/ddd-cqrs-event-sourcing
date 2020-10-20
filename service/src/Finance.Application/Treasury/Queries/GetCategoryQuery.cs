namespace Finance.Application.Treasury.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Core;
    using Dtos;
    using Microsoft.Data.SqlClient;

    public class GetCategoryQuery : IQuery<IList<GetCategoryDto>>
    {
        public GetCategoryQuery()
        {
        }
    }

    public class GetCategoryQueryHandler
        : IQueryHandler<GetCategoryQuery, IList<GetCategoryDto>>
    {
        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        public async Task<IList<GetCategoryDto>> HandleAsync(GetCategoryQuery args)
        {
            var sql = @"
                    select
                          id
                        , name
                    from category
                    order by name";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = await connection
                    .QueryAsync<GetCategoryDto>(sql);

                return result.ToList();
            }
        }
    }
}