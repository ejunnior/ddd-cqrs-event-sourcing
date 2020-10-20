namespace Finance.Tests.Infrastructure
{
    using Finance.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Net.Http;
    using Treasury.Api;
    using Treasury.Services;
    using IHost = Microsoft.Extensions.Hosting.IHost;

    public class FixtureBase
    {
        public static readonly Uri ApiHost = new Uri("http://localhost.com");
        private readonly TestServer _apiServer;
        private readonly DatabaseFactory _dbFactory = new DatabaseFactory();
        private readonly IHost _serviceHost;

        public FixtureBase()
        {
            _serviceHost = CreateServiceHost();
            _apiServer = CreateTestServer();
        }

        public SqlConnection Connection => _dbFactory.GetConnection();

        public MockServices MockServices { get; } = new MockServices();

        public IServiceProvider ServiceHostServices => _serviceHost.Services;

        internal static IConfiguration Configuration { get; private set; }

        public HttpClient CreateClient()
        {
            var client = _apiServer.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(10);

            return client;
        }

        private IHost CreateServiceHost()
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((
                    context,
                    config) =>
                {
                    context.HostingEnvironment.EnvironmentName = "Test";
                })
                .ConfigureServices(
                    services =>
                    {
                        services.AddTestDependencies();
                        MockServices.RegisterMockAsService(services);
                    }).Start();
        }

        private TestServer CreateTestServer()
        {
            var builder = new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder().Build())
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    //configBuilder.AddJsonFile(
                    //    path: "Gateway/Api/appsettings.json",
                    //    optional: false,
                    //    reloadOnChange: true);

                    Configuration = configBuilder.Build();

                    context.HostingEnvironment.EnvironmentName = "test";
                })
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureTestServices(services =>
                {
                    MockServices.RegisterMockAsService(services);
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                })
                .UseStartup(typeof(ApiTestStartup));

            return new TestServer(builder);
        }
    }
}