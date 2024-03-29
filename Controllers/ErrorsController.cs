using GraduationThesis_CarServices.Models.DTO.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [Route("/error")]
        // [HttpGet]
        public IActionResult Error()
        {
            switch (HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error!)
            {
                case MyException:
                    var exception = (MyException)HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error!;
                    return Problem(title: exception.Message, statusCode: exception.StatusCode);
                default:
                    return Problem(title: "Internal Server Error", statusCode: 500);
            }
        }
    }
}