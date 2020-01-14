using Microsoft.AspNetCore.Builder;

namespace MiddleWare
{
public static class CustomMiddlewareExtensions
{
public static IApplicationBuilder UseThirdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThirdMiddleware>();
        }
}
}