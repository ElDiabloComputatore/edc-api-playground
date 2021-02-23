using APIPlayground.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace APIPlayground.Controllers
{
    /// <summary>
    /// API versioning demo endpoints
    /// </summary>
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    public class EmptyController : EmptyBaseController
    {
        /// <summary>
        /// Return string in version 1.0
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [SwaggerResponse(200, Type = typeof(Empty))]
        public async Task<IActionResult> Get()
        {
            return await GetEmptyResult();
        }

        /// <summary>
        /// Return string in version 1.1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.1")]
        [SwaggerResponse(200, Type = typeof(Empty))]
        public async Task<IActionResult> GetV11()
        {
            return await GetEmptyResult();
        }

        /// <summary>
        /// Return version value from api-version header
        /// </summary>
        /// <returns></returns>
        [HttpGet("api-version")]
        [SwaggerResponse(200, Type = typeof(Empty))]
        public async Task<IActionResult> GetApiVersion()
        {
            return await GetEmptyResult();
        }

        private async Task<IActionResult> GetEmptyResult()
        {
            var model = new Empty
            {
                Value = GetApiVersionString()
            };

            return await Task.FromResult<IActionResult>(Ok(model));
        }
    }
}
