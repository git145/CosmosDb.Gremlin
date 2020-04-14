using CosmosDb.Gremlin.Core.Constants;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using System;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Domain.Services
{
    public class DatabaseService : IDatabaseService
    {
        /*private readonly IGremlinService _gremlinService;

        public DatabaseService(IGremlinService gremlinService)
        {
            _gremlinService = gremlinService;
        }*/

        public async Task DropGraph(GremlinClient gremlinClient)
        {
            Console.WriteLine("Dropping the graph");

            try
            {
                gremlinClient.SubmitAsync(GremlinCommands.DROP_GRAPH);

                Console.WriteLine("The graph was dropped successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not clear the graph: {e}");
            }
        }
    }
}
