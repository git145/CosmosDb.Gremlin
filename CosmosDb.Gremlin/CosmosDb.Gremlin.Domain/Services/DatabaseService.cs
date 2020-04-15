using CosmosDb.Gremlin.Core.Constants;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Domain.Services
{
    public class DatabaseService : IDatabaseService
    {
        public async Task<string> GetEdges(GremlinClient gremlinClient)
        {
            Console.WriteLine("Getting all vertices");

            string result = await ExecuteQuery(gremlinClient,
                GremlinCommands.GET_EDGES);

            return result;
        }

        public async Task<string> GetVertices(GremlinClient gremlinClient)
        {
            Console.WriteLine("Getting all vertices");

            string result = await ExecuteQuery(gremlinClient,
                GremlinCommands.GET_VERTICES);

            return result;
        }

        public async Task<string> DropEdges(GremlinClient gremlinClient)
        {
            Console.WriteLine("Dropping the edges");

            string result = await ExecuteQuery(gremlinClient,
                GremlinCommands.DROP_EDGES);

            return result;
        }

        public async Task<string> DropVertices(GremlinClient gremlinClient)
        {
            Console.WriteLine("Dropping vertices");

            string result = await ExecuteQuery(gremlinClient, 
                GremlinCommands.DROP_VERTICES);

            return result;
        }

        public async Task<string> ExecuteQuery(GremlinClient gremlinClient, 
            string query)
        {
            Console.WriteLine($"Excecuting the query \"{query}\"");

            string resultString = null;

            try
            {
                ResultSet<dynamic> result = await gremlinClient.SubmitAsync<dynamic>(query);

                resultString = JsonConvert.SerializeObject(result);

                Console.WriteLine("The query was executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not execute the query: {e}");
            }

            return resultString;
        }
    }
}
