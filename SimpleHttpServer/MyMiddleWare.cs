using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SimpleHttpServer
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;
        public MyMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
