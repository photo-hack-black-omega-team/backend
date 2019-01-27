using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhotoQuiz.Backend.Api.Controllers
{
    public class PhotoController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}