using Gremlin.Net.Driver;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Core.Interfaces.Services
{
    public interface IDatabaseService
    {
        Task<string> GetEdges(GremlinClient gremlinClient);

        Task<string> GetVertices(GremlinClient gremlinClient);

        Task<string> DropEdges(GremlinClient gremlinClient);

        Task<string> DropVertices(GremlinClient gremlinClient);

        Task<string> ExecuteQuery(GremlinClient gremlinClient,
            string query);
    }
}
