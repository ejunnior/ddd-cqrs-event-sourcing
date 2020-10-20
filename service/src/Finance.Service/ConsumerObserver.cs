namespace Finance.Service
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Data.UnitOfWork;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    //TODO: Room form improvements
    public class ConsumerObserver : IConsumeObserver
    {
        private readonly Serilog.ILogger _logger;

        public ConsumerObserver(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
            where T : class
        {
            var logMessage = new { Message = context.Message, Status = "Error" };
            string result = JsonConvert.SerializeObject(logMessage);
            _logger.Error(exception, result);
        }

        public async Task PostConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            var logMessage = new { Message = context.Message, Status = "Processed" };
            string result = JsonConvert.SerializeObject(logMessage);
            _logger.Information(result);
        }

        public async Task PreConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            var logMessage = new { Message = context.Message, Status = "Consumed" };
            string result = JsonConvert.SerializeObject(logMessage);
            _logger.Information(result);
        }
    }
}