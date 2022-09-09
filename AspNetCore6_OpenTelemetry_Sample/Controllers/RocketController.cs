using Microsoft.AspNetCore.Mvc;
using SomeApplicationProject;

namespace AspNetCore6_OpenTelemetry_Sample.Controllers
{
    public class RocketController : Controller
    {
        private readonly RocketService _service;
        private readonly ILogger<RocketController> _logger;

        public RocketController(RocketService service, ILogger<RocketController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Launch(int rocketId)
        {
            if (rocketId <= 0)
            {
                _logger.LogWarning("id <= 0");

                return BadRequest();
            }

            _logger.LogInformation("we're about to launch");
            var res = _service.LaunchRocketById(rocketId);

            _logger.LogInformation("lifoff");
            return Ok(res);
        }
    }
}
