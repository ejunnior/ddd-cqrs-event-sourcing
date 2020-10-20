namespace Finance.Domain.Core
{
    using System.Threading.Tasks;

    public interface IEventHandler<T>
           where T : IEvent
    {
        Task HandleAsync(T args);
    }
}