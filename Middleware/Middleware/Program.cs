using Middleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddlware>();
var app = builder.Build();


/*// app run only forward one request
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello");
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello2");
})*/


/*// chained middleware
// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello");
    await next(context);
});


// middleware 2
app.Use(async (context2, next) =>
{
    await context2.Response.WriteAsync("Hello2");
    await next(context2);
});


// middleware 3
app.Run(async (HttpContext context3) =>
{
    await context3.Response.WriteAsync("Hello3");

});
*/;

// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello\n");
    await next(context);
});


// middleware 2
// app.UseMiddleware<MyCustomMiddlware>();
// app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

// middleware 3
app.Run(async (HttpContext context3) =>
{
    await context3.Response.WriteAsync("Hello3\n");
    
});
app.Run();
