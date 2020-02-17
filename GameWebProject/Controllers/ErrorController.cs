using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebProject.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/statusCode")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The resource you requested could not be found";
                    break;
;            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.Exceptionmessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
