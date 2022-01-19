using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentSpy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> logger;

        public AdminController(ILogger<AdminController> log)
        {
            logger = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {                
                throw new Exception("Error occured");
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
        }
    }
}
