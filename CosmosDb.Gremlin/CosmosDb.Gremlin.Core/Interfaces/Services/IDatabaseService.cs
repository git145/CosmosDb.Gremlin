using Gremlin.Net.Driver;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.Core.Interfaces.Services
{
    public interface IDatabaseService
    {
        Task DropGraph(GremlinClient gremlinClient);
    }
}
