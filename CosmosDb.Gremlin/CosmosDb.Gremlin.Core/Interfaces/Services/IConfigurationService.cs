using Microsoft.Extensions.Configuration;

namespace CosmosDb.Gremlin.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
