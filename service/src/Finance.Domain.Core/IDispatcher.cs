namespace Finance.Domain.Core
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public interface IDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery args)
            where TQuery : IQuery<TResult>;

        Task DispatchAsync<T>(T args) where T : ICommand;
    }
}