namespace Finance.Api
{
    using Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)

        {
            app.Configure(
                hostingEnvironment: env,
                apiVersionDescriptionProvider: apiVersionDescriptionProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies()
                .RegisterServiceBus()
                .RegisterSwagger()
                .RegisterAuthentication()
                .AddCors()
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
                });
        }
    }
}