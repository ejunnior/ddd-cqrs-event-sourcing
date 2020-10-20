namespace Finance.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;

    public class MockServices
    {
        public MockServices()
        {
            Bus = new Mock<IBusControl>();
        }

        public Mock<IBusControl> Bus { get; }

        public void RegisterMockAsService(IServiceCollection services)
        {
            foreach (var mock in GetMocks())
            {
                var mockType = mock.GetType();
                var serviceType = mockType.GetGenericArguments().Single();

                services.Add(
                    new ServiceDescriptor(
                        serviceType,
                        instance: mock.Object
                    )
                );
            }
        }

        public void ResetAll()
        {
            foreach (var mock in GetMocks())
            {
                mock.Reset();
            }
        }

        private IEnumerable<Mock> GetMocks()
        {
            var mockBaseType = typeof(Mock);

            return GetType()
                .GetProperties()
                .Where(prop => prop.PropertyType.IsSubclassOf(mockBaseType))
                .Select(prop => prop.GetValue(this, null) as Mock);
        }
    }
}