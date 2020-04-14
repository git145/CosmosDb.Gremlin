using Gremlin.Net.Driver;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Core.Interfaces.Services
{
    public interface IDatabaseService
    {
        Task ExecuteQuery(GremlinClient gremlinClient,
            string query);

        Task DropEdges(GremlinClient gremlinClient);

        Task DropVertices(GremlinClient gremlinClient);
    }
}
