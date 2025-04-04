using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.myloggers;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "LoginForMicrosoftUsers", Roles = "Superadmin,Admin")] // this line denotes that all the endpoints are secured and to access them , authentication is necessary

    public class MicrosoftController : ControllerBase
    {
        private readonly ILogger<MicrosoftController> _logger;

        public MicrosoftController(ILogger<MicrosoftController> logger)
        {
            _logger = logger;
            
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors(PolicyName = "AllowMicrosoft")]
        public ActionResult Get()
        {
            //_mylogger.Log("simeple");
            //_logger.LogTrace("trace");

            //_logger.LogDebug("debug");
            //_logger.LogInformation("info");
            //_logger.LogWarning("warn");
            //_logger.LogError("error");
            //_logger.LogCritical("Critical");
            return Ok("this is  microsoft");
        }

    }
}