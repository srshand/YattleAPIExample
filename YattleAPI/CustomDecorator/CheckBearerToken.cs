using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace YattleAPI.CustomDecorator
{
    public class CheckBearerTokenAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var expectedToken = config["BearerToken"];

            // Log all headers for debugging
            foreach (var header in context.HttpContext.Request.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {header.Value}");
            }

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()
                ?? context.HttpContext.Request.Headers["authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            if (token != expectedToken)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
