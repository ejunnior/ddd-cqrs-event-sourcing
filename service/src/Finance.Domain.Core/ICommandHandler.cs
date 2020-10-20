namespace Finance.Domain.Core
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public interface ICommandHandler<T>
        where T : ICommand
    {
        Task HandleAsync(T args);
    }
}