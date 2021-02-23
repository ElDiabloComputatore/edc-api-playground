using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace APIPlayground.Utilities.Swagger
{
    public class ApiVersionHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "api-version",
                In = ParameterLocation.Header,
                AllowEmptyValue = false,
                Required = true,
                Description = "Enter API version in format: major.minor"
            });
        }
    }
}
