using CosmosDb.Gremlin.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace CosmosDb.Gremlin.Domain.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public IConfiguration GetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            AddConfigurationFile(configurationBuilder,
                "appsettings.json");

            // Remember to create this file as it is not committed
            AddConfigurationFile(configurationBuilder,
                "Configuration/GremlinConfiguration.json");

            IConfiguration configuration = configurationBuilder.Build();

            return configuration;
        }

        private void AddConfigurationFile(IConfigurationBuilder configurationBuilder,
            string filePath)
        {
            Console.WriteLine($"Looking for configuration file \"{filePath}\"");

            try
            {
                configurationBuilder.AddJsonFile(filePath);

                Console.WriteLine($"Successfully added configuration file \"{filePath}\"");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Failed to add configuration file \"{filePath}\": {e}");
            }
        }
    }
}
