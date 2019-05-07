namespace Utility.Error.Api.Configuration
{
    /// <summary>
    /// Service Configuration.
    /// </summary>
    public class ServiceConfiguration
    {
        // Connection String(s).
        public string ErrorsDbConnectionString { get; set; }

        // Services.
        public string UowPersistenceService { get; set; }
        public string CacheProviderService { get; set; }
        public string LoggerService { get; set; }

        // Flags.
        public bool UseInMemoryDb { get; set; }
        public bool UseDistributedCacheBehaviourPipeline { get; set; }
        public bool UseValidationBehaviourPipeline { get; set; }

    }
}
