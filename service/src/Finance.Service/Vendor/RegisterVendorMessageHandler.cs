namespace DistributedServices.Subscriber.Vendor
{
    using System.Threading.Tasks;
    using Application.Vendor.Commands;
    using Domain.Core;
    using Infrastructure.Messages.Vendor;
    using MassTransit;

    public class RegisterVendorMessageHandler
        : IConsumer<RegisterVendorMessage>
    {
        private readonly IDispatcher _dispatcher;

        public RegisterVendorMessageHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<RegisterVendorMessage> context)
        {
            await _dispatcher
                .DispatchAsync(new RegisterVendorCommand(
                    name: context.Message.Name,
                    mobilePhone: context.Message.MobilePhone,
                    email: context.Message.Email));
        }
    }
}