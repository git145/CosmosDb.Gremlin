using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CosmosDb.Gremlin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly IGremlinService _gremlinService;

        private readonly IDatabaseService _databaseService;

        public DatabaseController(IGremlinService gremlinService,
            IDatabaseService databaseService)
        {
            _gremlinService = gremlinService;

            _databaseService = databaseService;
        }

        // GET api/<controller>
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World!");
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            return Ok("Hello World!");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, 
            [FromBody]string value)
        {
            return Ok("Hello World!");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok("Hello World!");
        }

        // DELETE api/<controller>/clear
        [HttpGet("drop-graph")]
        public async Task DropGraph()
        {
            Console.WriteLine("Received a call to drop the graph");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    await _databaseService.DropVertices(gremlinClient);
                }
            }
            else
            {
                Console.WriteLine("ERROR - Could not clear the graph as the gremlin server object is null");
            }
        }
    }
}
