using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> _logger)
        {
            logger = _logger;
        }

        public IActionResult Index([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            logger.LogError("Error", new 
            {
                exceptionHandlerFeature.Endpoint,
                exceptionHandlerFeature.Path,
                exceptionHandlerFeature.RouteValues,
                ErrorMessage = exceptionHandlerFeature.Error.Message,
                ErrorData = exceptionHandlerFeature.Error.Data,
                ErrorStackTrace = exceptionHandlerFeature.Error.StackTrace,
            });

            return Problem();
        }
    }
}
