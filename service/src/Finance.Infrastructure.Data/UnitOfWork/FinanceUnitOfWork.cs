namespace Finance.Infrastructure.Data.UnitOfWork
{
    using Core;
    using Newtonsoft.Json;
    using Raven.Client.Documents;
    using Raven.Client.Documents.Conventions;
    using Raven.Client.Documents.Session;
    using Raven.Client.Json.Serialization.NewtonsoftJson;
    using System;
    using System.Threading.Tasks;

    public class FinanceUnitOfWork : IFinanceUnitOfWork
    {
        private static readonly IDocumentStore SessionFactory;
        private readonly IAsyncDocumentSession _session;

        static FinanceUnitOfWork()
        {
            SessionFactory = BuildSessionFactory();
        }

        public FinanceUnitOfWork()
        {
            _session = SessionFactory.OpenAsyncSession();
        }

        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__RavenDbConnectionString");

        public IAsyncDocumentSession CreateSet()
        {
            return _session;
        }

        public async Task SaveChangesAsync()
        {
            await _session.SaveChangesAsync();
            _session.Dispose();
        }

        private static IDocumentStore BuildSessionFactory()
        {
            var store = new DocumentStore
            {
                Conventions = new DocumentConventions
                {
                    Serialization = new NewtonsoftJsonSerializationConventions
                    {
                        CustomizeJsonSerializer = serializer =>
                            {
                                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                                serializer.ContractResolver = new SerializeContractResolver();
                            },
                        //CustomizeJsonDeserializer = deserializer =>
                        //    {
                        //        deserializer.ContractResolver = new DeserializeContractResolver();
                        //    }
                    }
                },
                Urls = new[] { ConnectionString },
                Database = "Db_Finance"
            }.Initialize();

            store.OnAfterSaveChanges += Store_OnAfterSaveChanges;

            return store;
        }

        private static void Store_OnAfterSaveChanges(object sender, AfterSaveChangesEventArgs e)
        {
            //Dispatch the event...
            var a = 1;
        }
    }
}