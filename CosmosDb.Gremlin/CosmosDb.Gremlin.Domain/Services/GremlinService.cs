using CosmosDb.Gremlin.Core.Interfaces.Models.Configuration;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using System;

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
            Console.WriteLine("Creating new gremlin service");

            InitialiseGremlinServer(gremlinServerConfiguration);
        }

        private void InitialiseGremlinServer(IGremlinConfigurationModel gremlinServerConfiguration)
        {
            Console.WriteLine("Initialising new gremlin server");

            try
            {
                MyGremlinServer = new GremlinServer(gremlinServerConfiguration.Hostname,
                    gremlinServerConfiguration.Port,
                    gremlinServerConfiguration.IsSsl,
                    $"/dbs/{gremlinServerConfiguration.Database}/colls/{gremlinServerConfiguration.Graph}",
                    gremlinServerConfiguration.MasterKey);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Failed to initialise the gremlin server: {e}");
            }
        }
    }
}
