using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.AspNetCore.Mvc;
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

        // GET api/<controller>/edges
        [HttpGet("edges")]
        public async Task<IActionResult> GetEdges()
        {
            IActionResult response = BadRequest("Could not get edges");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    string result = await _databaseService.GetEdges(gremlinClient);

                    if (!string.IsNullOrEmpty(result))
                    {
                        response = Ok(result);
                    }
                }
            }

            return response;
        }

        // GET api/<controller>/vertices
        [HttpGet("vertices")]
        public async Task<IActionResult> GetVertices()
        {
            IActionResult response = BadRequest("Could not get vertices");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    string result = await _databaseService.GetVertices(gremlinClient);

                    if (!string.IsNullOrEmpty(result))
                    {
                        response = Ok(result);
                    }
                }
            }

            return response;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string query)
        {
            IActionResult response = BadRequest("The query was invalid");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    string result = await _databaseService.ExecuteQuery(gremlinClient, 
                        query);

                    if(!string.IsNullOrEmpty(result))
                    {
                        response = Ok(result);
                    }
                }
            }

            return response;
        }

        // DELETE api/<controller>/drop/edges
        [HttpDelete("drop/edges")]
        public async Task<IActionResult> DropEdges()
        {
            IActionResult response = BadRequest("Failed to drop the edges");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    string result = await _databaseService.DropEdges(gremlinClient);

                    if (!string.IsNullOrEmpty(result))
                    {
                        response = Ok(result);
                    }
                }
            }

            return response;
        }

        // DELETE api/<controller>/drop/vertices
        [HttpDelete("drop/vertices")]
        public async Task<IActionResult> DropVertices()
        {
            IActionResult response = BadRequest("Failed to drop the vertices");

            if (_gremlinService.MyGremlinServer != null)
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType))
                {
                    string result = await _databaseService.DropVertices(gremlinClient);

                    if (!string.IsNullOrEmpty(result))
                    {
                        response = Ok(result);
                    }
                }
            }

            return response;
        }
    }
}
