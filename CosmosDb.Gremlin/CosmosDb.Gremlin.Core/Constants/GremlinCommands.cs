namespace CosmosDb.Gremlin.Core.Constants
{
    public static class GremlinCommands
    {
        public const string DROP_EDGES = "g.E().drop()";

        public const string DROP_VERTICES = "g.V().drop()";

        public const string GET_EDGES = "g.E()";

        public const string GET_VERTICES = "g.V()";
    }
}
