using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.myloggers;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "LoginForGoogleUsers", Roles = "Superadmin,Admin,Student")] // this line denotes that all the endpoints are secured and to access them , authentication is necessary

    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            //_mylogger.Log("simeple");
            _logger.LogTrace("trace");

            _logger.LogDebug("debug");
            _logger.LogInformation("info");
            _logger.LogWarning("warn");
            _logger.LogError("error");
            _logger.LogCritical("Critical");
            return Ok();
        }
    }
}
