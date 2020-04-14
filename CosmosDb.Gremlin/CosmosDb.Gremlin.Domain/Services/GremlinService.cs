using CosmosDb.Gremlin.Core.Interfaces.Models.Configuration;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using System;
using System.Diagnostics;

namespace CosmosDb.Gremlin.Domain.Services
{
    public class GremlinService : IGremlinService
    {
        public GremlinServer MyGremlinServer { get; private set; }

        public GremlinService(IGremlinConfigurationModel gremlinServerConfiguration) {
            Initialise(gremlinServerConfiguration);
        }

        private void Initialise(IGremlinConfigurationModel gremlinServerConfiguration)
        {
            try
            {
                MyGremlinServer = new GremlinServer(gremlinServerConfiguration.Hostname,
                    gremlinServerConfiguration.Port,
                    gremlinServerConfiguration.IsSsl,
                    gremlinServerConfiguration.Username,
                    gremlinServerConfiguration.MasterKey);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error creating the gremlin server: {e}");
            }
        }
    }
}
