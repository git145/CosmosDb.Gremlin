﻿using CosmosDb.Gremlin.Core.Interfaces.Models.Configuration;
using CosmosDb.Gremlin.Core.Interfaces.Services;
using CosmosDb.Gremlin.Core.Models.Configuration;
using CosmosDb.Gremlin.Domain.Services;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CosmosDb.Gremlin.ScriptRunner
{
    class Program
    {
        private static readonly IDatabaseService _databaseService = new DatabaseService();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting the script runner program");

            IConfigurationService configurationService = new ConfigurationService();

            IConfiguration configuration = configurationService.GetConfiguration();

            IGremlinConfigurationModel gremlinConfigurationModel = new GremlinConfigurationModel();

            configuration.GetSection("Gremlin").Bind(gremlinConfigurationModel);

            gremlinConfigurationModel.Graph = "Airport";

            IGremlinService gremlinService = new GremlinService(gremlinConfigurationModel);

            await PopulateGraph(gremlinService);
        }

        private static async Task PopulateGraph(IGremlinService gremlinService)
        {
            Console.WriteLine("Populating the graph");

            try
            {
                using (var gremlinClient = new GremlinClient(gremlinService.MyGremlinServer,
                        new GraphSON2Reader(),
                        new GraphSON2Writer(),
                        GremlinClient.GraphSON2MimeType))
                {
                    await _databaseService.DropVertices(gremlinClient);

                    // --- Terminal 1 ---

                    // V: Terminal 1
                    await CreateTerminal(gremlinClient, "Terminal 1");

                    // V: Gates in terminal 1
                    await CreateGate(gremlinClient, "Gate T1-1");
                    await CreateGate(gremlinClient, "Gate T1-2");
                    await CreateGate(gremlinClient, "Gate T1-3");

                    // V: Restaurants in terminal 2
                    await CreateRestaurant(gremlinClient, "Wendys", 0.4m, 9.5m);
                    await CreateRestaurant(gremlinClient, "McDonalds", 0.3m, 8.15m);
                    await CreateRestaurant(gremlinClient, "Chipotle", 0.6m, 12.5m);

                    // E: TerminalToGate (cyan)
                    await CreateTerminalToGate(gremlinClient, "Terminal 1", "Gate T1-1", 3);
                    await CreateTerminalToGate(gremlinClient, "Terminal 1", "Gate T1-2", 5);
                    await CreateTerminalToGate(gremlinClient, "Terminal 1", "Gate T1-3", 7);

                    // E: TerminalToRestaurant (purple)
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 1", "Wendys", 5);
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 1", "McDonalds", 7);
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 1", "Chipotle", 10);

                    // E: GateToNextGate / GateToPrevGate (cyan dashed)
                    await CreateGateToGate(gremlinClient, "Gate T1-1", "Gate T1-2", 2);
                    await CreateGateToGate(gremlinClient, "Gate T1-2", "Gate T1-3", 2);

                    // E: GateToRestaurant (purple dashed)
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-1", "Wendys", 2);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-1", "McDonalds", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-1", "Chipotle", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-2", "Wendys", 2);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-2", "McDonalds", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-2", "Chipotle", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-3", "Wendys", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-3", "McDonalds", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T1-3", "Chipotle", 2);

                    // --- Terminal 2 ---

                    // V: Terminal 2
                    await CreateTerminal(gremlinClient, "Terminal 2");

                    // V: Gates in terminal 2
                    await CreateGate(gremlinClient, "Gate T2-1");
                    await CreateGate(gremlinClient, "Gate T2-2");
                    await CreateGate(gremlinClient, "Gate T2-3");

                    // V: Restaurants in terminal 2
                    await CreateRestaurant(gremlinClient, "Jack in the Box", 0.3m, 3.15m);
                    await CreateRestaurant(gremlinClient, "Kentucky Fried Chicken", 0.4m, 7.5m);
                    await CreateRestaurant(gremlinClient, "Burger King", 0.2m, 7.15m);

                    // E: TerminalToGate
                    await CreateTerminalToGate(gremlinClient, "Terminal 2", "Gate T2-1", 3);
                    await CreateTerminalToGate(gremlinClient, "Terminal 2", "Gate T2-2", 5);
                    await CreateTerminalToGate(gremlinClient, "Terminal 2", "Gate T2-3", 7);

                    // E: TerminalToRestaurant
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 2", "Jack in the Box", 5);
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 2", "Kentucky Fried Chicken", 7);
                    await CreateTerminalToRestaurant(gremlinClient, "Terminal 2", "Burger King", 10);

                    // E: GateToNextGate / GateToPrevGate
                    await CreateGateToGate(gremlinClient, "Gate T2-1", "Gate T2-2", 2);
                    await CreateGateToGate(gremlinClient, "Gate T2-2", "Gate T2-3", 2);

                    // E: GateToRestaurant
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-1", "Jack in the Box", 2);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-1", "Kentucky Fried Chicken", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-1", "Burger King", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-2", "Jack in the Box", 2);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-2", "Kentucky Fried Chicken", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-2", "Burger King", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-3", "Jack in the Box", 6);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-3", "Kentucky Fried Chicken", 4);
                    await CreateGateToRestaurant(gremlinClient, "Gate T2-3", "Burger King", 2);

                    // --- Terminal to Terminal ---

                    // E: TerminalToNextTerminal / TerminalToPrevTerminal
                    await CreateTerminalToTerminal(gremlinClient, "Terminal 1", "Terminal 2", 10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR - Could not populate the graph: {e}");
            }
        }

        private static async Task CreateTerminal(GremlinClient client, 
            string id)
        {
            var gremlinCode = $@"
				g.addV('terminal')
					.property('id', '{id}')
					.property('city', 'LA')
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created vertex: Terminal '{id}'");
        }

        private static async Task CreateGate(GremlinClient client, 
            string id)
        {
            var gremlinCode = $@"
				g.addV('gate')
					.property('id', '{id}')
					.property('city', 'LA')
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created vertex: Gate '{id}'");
        }

        private static async Task CreateRestaurant(GremlinClient client, 
            string id, 
            decimal rating, 
            decimal averagePrice)
        {
            var gremlinCode = $@"
				g.addV('restaurant')
					.property('id', '{id}')
					.property('city', 'LA')
					.property('rating', {rating})
					.property('averagePrice', {averagePrice})
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created vertex: Restaurant '{id}'");
        }

        private static async Task CreateTerminalToGate(GremlinClient client, 
            string terminal, 
            string gate, 
            int distanceInMinutes)
        {
            var gremlinCode = $@"
				g.V()
					.has('id', '{terminal}')
					.addE('terminalToGate')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{gate}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: TerminalToGate '{terminal}' > '{gate}'");

            gremlinCode = $@"
				g.V()
					.has('id', '{gate}')
					.addE('gateToTerminal')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{terminal}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: GateToTerminal '{gate}' > '{terminal}'");
        }

        private static async Task CreateTerminalToRestaurant(GremlinClient client, 
            string terminal, 
            string restaurant, 
            int distanceInMinutes)
        {
            var gremlinCode = $@"
				g.V()
					.has('id', '{terminal}')
					.addE('terminalToRestaurant')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{restaurant}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: TerminalToRestaurant '{terminal}' > '{restaurant}'");

            gremlinCode = $@"
				g.V()
					.has('id', '{restaurant}')
					.addE('restaurantToTerminal')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{terminal}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: RestaurantToTerminal '{restaurant}' > '{terminal}'");
        }

        private static async Task CreateGateToGate(GremlinClient client, 
            string gate1, 
            string gate2, 
            int distanceInMinutes)
        {
            var gremlinCode = $@"
				g.V()
					.has('id', '{gate1}')
					.addE('gateToNextGate')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{gate2}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: GateToNextGate '{gate1}' > '{gate2}'");

            gremlinCode = $@"
				g.V()
					.has('id', '{gate2}')
					.addE('gateToPrevGate')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{gate1}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: GateToPrevGate '{gate2}' > '{gate1}'");
        }

        private static async Task CreateGateToRestaurant(GremlinClient client, 
            string gate, 
            string restaurant, 
            int distanceInMinutes)
        {
            var gremlinCode = $@"
				g.V()
					.has('id', '{gate}')
					.addE('gateToRestaurant')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{restaurant}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: GateToRestaurant '{gate}' > '{restaurant}'");

            gremlinCode = $@"
				g.V()
					.has('id', '{restaurant}')
					.addE('restaurantToGate')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{gate}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: RestaurantToGate '{restaurant}' > '{gate}'");
        }

        private static async Task CreateTerminalToTerminal(GremlinClient client, 
            string terminal1, 
            string terminal2, 
            int distanceInMinutes)
        {
            var gremlinCode = $@"
				g.V()
					.has('id', '{terminal1}')
					.addE('terminalToNextTerminal')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{terminal2}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: TerminalToNextTerminal '{terminal1}' > '{terminal2}'");

            gremlinCode = $@"
				g.V()
					.has('id', '{terminal2}')
					.addE('terminalToPrevTerminal')
					.property('distanceInMinutes', {distanceInMinutes})
					.to(
						g.V()
							.has('id', '{terminal1}'))
			";

            await _databaseService.ExecuteQuery(client, gremlinCode);

            Console.WriteLine($"Created edge: TerminalToPrevTerminal '{terminal2}' > '{terminal1}'");
        }
    }
}
