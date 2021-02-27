using APIPlayground.Models;
using APIPlaygroundBusiness;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace APIPlayground.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessCalculator _businessCalculator;

        public BusinessController(IBusinessCalculator businessCalculator)
        {
            _businessCalculator = businessCalculator;
        }

        [HttpGet("business-value")]
        [SwaggerResponse(200, Type = typeof(BusinessResponse))]
        public async Task<IActionResult> GetBusinessValue()
        {
            var result = new BusinessResponse
            {
                Value = _businessCalculator.CalculateBusiness()
            };

            return await Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
