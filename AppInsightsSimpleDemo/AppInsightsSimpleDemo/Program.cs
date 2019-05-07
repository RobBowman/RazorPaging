using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AppInsightsSimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create DI container.
            IServiceCollection services = new ServiceCollection();

            // Channel is explicitly configured to do flush on it later.
            var channel = new InMemoryChannel();
            services.Configure<TelemetryConfiguration>(
                (config) =>
                {
                    config.TelemetryChannel = channel;
                }
            );

            // Add the logging pipelines to use. We are using Application Insights only here.
            services.AddLogging(builder =>
            {
                // Optional: Apply filters to configure LogLevel Trace or above is sent to
                // Application Insights for all categories.
                builder.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                                 ("", LogLevel.Trace);
                builder.AddApplicationInsights("4f396f07-9af6-4331-80c8-5cf667166e52");
            });

            // Build ServiceProvider.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            // Begin a new scope. This is optional.
            using (logger.BeginScope(new Dictionary<string, object> { { "Method", nameof(Main) } }))
            {
                logger.LogInformation("Logger is working"); // this will be captured by Application Insights.
                Exception ex = new Exception("a custom exception");
                logger.LogError(ex, "This is a message from the console app");
            }

            // Explicitly call Flush() followed by sleep is required in Console Apps.
            // This is to ensure that even if application terminates, telemetry is sent to the back-end.
            channel.Flush();
            Thread.Sleep(1000);
        }
    }
}
