using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddleWare
{
    public class ThirdMiddleware
    {
        private readonly RequestDelegate _next;

        public ThirdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(ThirdMiddleware)} in. \r\n");

            await _next(context);

            await context.Response.WriteAsync($"{nameof(ThirdMiddleware)} out. \r\n");
        }
    }
}