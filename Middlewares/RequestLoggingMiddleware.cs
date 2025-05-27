using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StiegerInmobiliaria.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] {context.Request.Method} {context.Request.Path}");

            await _next(context);
        }
    }
}
