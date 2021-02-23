using Microsoft.AspNetCore.Mvc;

namespace APIPlayground.Controllers
{
    [ApiController]
    public class EmptyBaseController : ControllerBase
    {
        protected string GetApiVersionString()
        {
            return HttpContext.GetRequestedApiVersion().ToString();
        }
    }
}
