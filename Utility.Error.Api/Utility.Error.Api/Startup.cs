using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using MediatR.Pipeline;

using Utility.Error.Api.Configuration;
using Utility.Error.Api.Extensions;
using Utility.Error.Application.Interfaces;
using Utility.Error.Persistence;

namespace Utility.Error.Api
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        private readonly ILogger _logger;
        private ServiceConfiguration _serviceConfiguration;

        // Properties.
        #region Properties

        public IConfigurationRoot Configuration { get; private set; }

        #endregion

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public Startup(IHostingEnvironment env, ILogger<Startup> logger)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            _logger = logger;
        }

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Services to the collection.
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Get Configuration.
            ReadServiceConfiguration();

            // Add framework services.
            services.AddScoped(typeof(IUnitOfWork), Type.GetType(_serviceConfiguration.UowPersistenceService));
            services.AddTransient(typeof(ILoggerService), Type.GetType(_serviceConfiguration.LoggerService));

            // Initialise Data Source - (InMemory/SQL).
            if (_serviceConfiguration.UseInMemoryDb)
            {
                // Register Db Context.
                #pragma warning disable 618
                services.AddDbContext<MockDbContext>(options => options.UseInMemoryDatabase());
                #pragma warning restore 618
            }
            else
            {
                // Add Db Context.
                services.AddDbContext<ErrorsDbContext>(options => options.UseSqlServer(_serviceConfiguration.ErrorsDbConnectionString));
            }

            // =======================
            // MediatR Initialisation.
            // =======================
            services.AddScoped(typeof(IMediator), typeof(Mediator));
            services.AddTransient<ServiceFactory>(p => p.GetService);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            // Register all commands/queries/validators.
            services.AddMediatR(typeof(Application.Error.Queries.ByIdentifier.Query).GetTypeInfo().Assembly);

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development Environment!");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMvc();
        }

        #endregion

        // Private Methods.
        #region PrivateMethods

        /// <summary>
        /// ReadServiceConfiguration
        /// </summary>
        private void ReadServiceConfiguration()
        {
            _serviceConfiguration = new ServiceConfiguration();
            Configuration.GetSection("ServiceConfiguration").Bind(_serviceConfiguration);

            // Validate Configuration.

            // Persistence Service.
            if (string.IsNullOrEmpty(_serviceConfiguration.UowPersistenceService))
            {
                throw new Exception("persistence service configuration key cannot be null or empty!");
            }

            // Logger Service.
            if (string.IsNullOrEmpty(_serviceConfiguration.LoggerService))
            {
                throw new Exception("logger service configuration key cannot be null or empty!");
            }

            // Using Database?
            if (_serviceConfiguration.UseInMemoryDb)
            {
                // Connection String.
                if (string.IsNullOrEmpty(_serviceConfiguration.ErrorsDbConnectionString))
                {
                    throw new Exception("errors db connection string key cannot be null or empty when using Db!");
                }
            }

            // Using Distributed Cache.
            if (_serviceConfiguration.UseDistributedCacheBehaviourPipeline)
            {
                // Cache Provider Service.
                if (string.IsNullOrEmpty(_serviceConfiguration.CacheProviderService))
                {
                    throw new Exception("cache provider service cannot be null or empty when distributed cache!");
                }
            }
        }

        #endregion
    }
}
