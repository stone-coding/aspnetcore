using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.MapGet("/", () => "Hello World!");*/

/*app.Run(async (HttpContext context) =>
{
    if ( 1== 1)
    {
        context.Response.StatusCode = 200;
    } else
    {
        context.Response.StatusCode = 400;
    }
    
    await context.Response.WriteAsync("Hello");
    await context.Response.WriteAsync("World");
});*/

/*app.Run(async (HttpContext context) =>
{
    context.Response.Headers["myKey"] = "my value";
    context.Response.Headers["Server"] = "my server";
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<h3>Hello</h3>");
    await context.Response.WriteAsync("<h4>World</h4>");
});*/


//display request path and method
/*app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync($"<p>{path}</p>");
    await context.Response.WriteAsync($"<p>{method}</p>");
});*/


//check request header 
/*app.Run(async (HttpContext context) =>
{

    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }
    }

});*/

// see user-Agent in request header
/*app.Run(async (HttpContext context) =>
{

        context.Response.Headers["Content-Type"] = "text/html";
        if (context.Request.Headers.ContainsKey("User-Agent"))
        {
            string userAgent = context.Request.Headers["User-Agent"];
            await context.Response.WriteAsync($"<p>{userAgent}</p>");
        }
    

});*/

// test postman
/*app.Run(async (HttpContext context) =>
{

    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Headers.ContainsKey("AuthorizationKey"))
    {
        string auth = context.Request.Headers["AuthorizationKey"];
        await context.Response.WriteAsync($"<p>{auth}</p>");
    }


});*/

app.Run(async (HttpContext context) =>
{

    StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    // convert string to dict
    Dictionary<string, StringValues> queryDict 
    = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if (queryDict.ContainsKey("firstName"))
    {
        string firstName = queryDict["firstName"][0];
        await context.Response.WriteAsync(firstName);
    }

    if (queryDict.ContainsKey("age"))
    {
        string firstName = queryDict["age"][0];
        foreach (var item in queryDict["age"])
        {
            await context.Response.WriteAsync(item);
        }
    }

});
app.Run();
