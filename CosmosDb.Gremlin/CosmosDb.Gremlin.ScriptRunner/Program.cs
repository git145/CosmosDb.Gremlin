using CosmosDb.Gremlin.Core.Interfaces.Services;
using CosmosDb.Gremlin.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace CosmosDb.Gremlin.ScriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationService configurationService = new ConfigurationService();

            IConfiguration configuration = configurationService.GetConfiguration();
        }
    }
}
