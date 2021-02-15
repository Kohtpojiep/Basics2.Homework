using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Basics2.Homework.Api.Controllers
{
    public class TestingMiddleware
    {
        private readonly RequestDelegate _next;
 
        public TestingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token!="12345678")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}   