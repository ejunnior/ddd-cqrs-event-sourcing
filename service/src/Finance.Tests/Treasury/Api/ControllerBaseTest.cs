namespace Finance.Tests.Treasury.Api
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using Infrastructure;
    using Microsoft.Data.SqlClient;
    using Xunit;

    [Collection(FixtureBaseCollection.Name)]
    public abstract class ControllerBaseTest<TFixture>
            where TFixture : FixtureBase
    {
        private readonly TFixture _fixture;

        public ControllerBaseTest(TFixture fixture)
        {
            _fixture = fixture;

            _fixture.MockServices.ResetAll();

            HttpClient = _fixture.CreateClient();
        }

        protected SqlConnection Connection => _fixture.Connection;

        protected MockServices MockServices => _fixture.MockServices;

        protected HttpClient HttpClient { get; }

        protected virtual Uri GetUri(string path, NameValueCollection queryString = null)
        {
            var builder = new UriBuilder(FixtureBase.ApiHost)
            {
                Path = path
            };

            if (queryString != null)
            {
                builder.Query = queryString.ToQueryString();
            }

            return builder.Uri;
        }
    }
}