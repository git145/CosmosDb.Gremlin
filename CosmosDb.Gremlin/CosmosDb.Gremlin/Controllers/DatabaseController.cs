using CosmosDb.Gremlin.Core.Interfaces.Services;
using Gremlin.Net.Driver;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CosmosDb.Gremlin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly IGremlinService _gremlinService;

        public DatabaseController(IGremlinService gremlinService)
        {
            _gremlinService = gremlinService;
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
        [HttpGet("clear")]
        public async Task Clear()
        {
            if (_gremlinService.MyGremlinServer != null) 
            {
                using (GremlinClient gremlinClient = new GremlinClient(_gremlinService.MyGremlinServer))
                {
                    string gremlinCode = "g.V()";

                    try
                    {
                        await gremlinClient.SubmitAsync(gremlinCode);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Could not delete the graph: {e}");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Could not complete the operation as the gremlin server object is null.");
            }
        }
    }
}
