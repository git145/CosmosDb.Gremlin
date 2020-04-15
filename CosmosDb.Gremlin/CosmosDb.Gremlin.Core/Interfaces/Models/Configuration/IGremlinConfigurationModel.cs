namespace CosmosDb.Gremlin.Core.Interfaces.Models.Configuration
{
    public interface IGremlinConfigurationModel
    {
        string Hostname { get; }

        int Port { get; }

        bool IsSsl { get; }

        string Database { get; }

        string Graph { get; set; }

        string MasterKey { get; }
    }
}
