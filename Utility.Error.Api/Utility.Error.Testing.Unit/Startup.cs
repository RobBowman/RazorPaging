using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatR.Pipeline;

using Utility.Error.Application.Interfaces;
using Utility.Error.Persistence;
using Utility.Error.Testing.Unit.Controller;

namespace Utility.Error.Testing.Unit
{
    /// <summary>
    /// Startup.
    /// </summary>
    public static class Startup
    {
        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Initialise
        /// </summary>
        /// <returns></returns>
        public static ServiceProvider Initialise()
        {
            // Initialise Services Collection.
            var services = new ServiceCollection();

            // Configure/Register Services.
            ConfigureServices(services);

            // Return Services Provider.
            return services.BuildServiceProvider();
        }

        #endregion

        // Private Methods.
        #region PrivateMethods

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Read Service Types To Register.
            // Typically read from configuration or lookup data store.
            var uowpersistenceservice = "Utility.Error.Persistence.Implementation.InMemoryUow, Utility.Error.Persistence";
            var loggerservice = "Utility.Error.Infrastructure.Logging.TraceLoggerService, Utility.Error.Infrastructure";

            // Add framework services.
            services.AddScoped(typeof(IUnitOfWork), Type.GetType(uowpersistenceservice));
            services.AddTransient(typeof(ILoggerService), Type.GetType(loggerservice));

            // Register Db Context.
            #pragma warning disable 618
            services.AddDbContext<MockDbContext>(options => options.UseInMemoryDatabase());
            #pragma warning restore 618

            // =======================
            // MediatR Initialisation.
            // =======================
            services.AddScoped(typeof(IMediator), typeof(Mediator));
            services.AddTransient<ServiceFactory>(p => p.GetService);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            // ==================================
            // MediatR Extensions Initialisation.
            // ==================================
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            // Register all commands/queries/validators.
            services.AddMediatR(typeof(Application.Error.Queries.ByIdentifier.Query).GetTypeInfo().Assembly);

            // Finally register controller.
            services.AddTransient(typeof(BaseController), typeof(ErrorController));
        }

        #endregion
    }
}