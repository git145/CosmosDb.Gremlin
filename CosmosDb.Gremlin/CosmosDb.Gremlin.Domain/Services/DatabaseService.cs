using CosmosDb.Gremlin.Core.Constants;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using System;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Domain.Services
{
    public class DatabaseService : IDatabaseService
    {
        public async Task ExecuteQuery(GremlinClient gremlinClient, 
            string query)
        {
            Console.WriteLine($"Excecuting the query \"{query}\"");

            try
            {
                await gremlinClient.SubmitAsync(query);

                Console.WriteLine("The query was executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not execute the query: {e}");
            }
        }

        public async Task DropEdges(GremlinClient gremlinClient)
        {
            Console.WriteLine("Dropping the edges");

            try
            {
                await gremlinClient.SubmitAsync(GremlinCommands.DROP_EDGES);

                Console.WriteLine("The edges were dropped successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not drop the edges: {e}");
            }
        }

        public async Task DropVertices(GremlinClient gremlinClient)
        {
            Console.WriteLine("Dropping vertices");

            try
            {
                await gremlinClient.SubmitAsync(GremlinCommands.DROP_VERTICES);

                Console.WriteLine("The vertices were dropped successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not drop the vertices {e}");
            }
        }
    }
}
