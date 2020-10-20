namespace Finance.Tests.Infrastructure
{
    using System;
    using Microsoft.Data.SqlClient;

    internal class DatabaseFactory : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly LaunchSettingsFixture launchSettings = new LaunchSettingsFixture();

        public DatabaseFactory()
        {
            _connection =
                new SqlConnection(ConnectionString);
        }

        private static string ConnectionString =>
            Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        void IDisposable.Dispose()
        {
            _connection.Dispose();
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}