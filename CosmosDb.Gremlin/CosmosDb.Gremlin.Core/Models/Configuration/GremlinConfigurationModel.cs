using CosmosDb.Gremlin.Core.Interfaces.Models.Configuration;

namespace CosmosDb.Gremlin.Core.Models.Configuration
{
    public class GremlinConfigurationModel : IGremlinConfigurationModel
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public bool IsSsl { get; set; }

        public string Username { get; set; }

        public string MasterKey { get; set; }
    }
}
