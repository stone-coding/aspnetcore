namespace Middleware.CustomMiddleware
{
    public class MyCustomMiddlware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My Custom Middleware - start\n");
            await next(context);
            await context.Response.WriteAsync("My Custom Middleware - end\n");
        }
    }

    public static class CustomMiddlewareExtension
    {
        // c# exntesion method inject the extension to the object
        // for example following method injcxt the IApplicationBuilder to the app
        // and you will access this object by variable app
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
           return app.UseMiddleware<MyCustomMiddlware>();
        }
    }
}
