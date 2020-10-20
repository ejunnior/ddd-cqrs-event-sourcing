namespace Finance.Infrastructure.Data.Core
{
    using System.Threading.Tasks;
    using Domain.Core;
    using Raven.Client.Documents.Session;

    public interface IQueryableUnitOfWork
          : IUnitOfWork
    {
        IAsyncDocumentSession CreateSet();
    }
}