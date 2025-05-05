using System.Web.Http;

namespace RsDisplayConsole{
    
    public class HelloController : ApiController
    {
        [HttpGet]
        [Route("api/hello")]
        public IHttpActionResult SayHello()
        {
            return Ok("Hello from .NET HTTP Server!");
        }
    }
}