using Gremlin.Net.Driver;

namespace CosmosDb.Gremlin.Core.Interfaces.Services
{
    public interface IGremlinService
    {
        GremlinServer MyGremlinServer { get; }
    }
}
