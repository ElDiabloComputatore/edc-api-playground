﻿using Microsoft.AspNetCore.Mvc;

namespace APIPlayground.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class EmptyBaseController : ControllerBase
    {
        protected string GetApiVersionString()
        {
            return HttpContext.GetRequestedApiVersion().ToString();
        }
    }
}
