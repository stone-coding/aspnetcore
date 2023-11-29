using Routing.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

// add predefined custom constraint for the month 
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months",typeof(MonthsCustomConstraints));
});


var app = builder.Build();


//enable routing
// middleware intercept http request and send back http respond by editing request and response
// routing is check http method(get,set) and url to navigate to http 
/*
app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if(endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint:{endpoint.DisplayName}\n");
    }
    await next(context);
});

app.UseRouting();

app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint:{endpoint.DisplayName}\n");
    }
    await next(context);
});
app.UseEndpoints(endpoints =>
{
    //add your end points
    endpoints.MapGet("map1", async (context) =>
    {
        await context.Response.WriteAsync("In map 1");
    });

    endpoints.MapPost("map2", async (context) =>
    {
        await context.Response.WriteAsync("In map 2");
    });
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});*/









app.UseRouting();


app.UseEndpoints(endpoints =>
{
    //add your end points
    //files is the fixed and filename and extensions are parameter since they can be changed 
    endpoints.Map("files/{filename}.{extension}", async (context) =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"In files - {fileName} - {extension}");
    });

    //default paramter 
    endpoints.Map("employee/profile/{name:length(4,7):alpha=user}", async (context) =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["name"]);
        await context.Response.WriteAsync($"In employee - {employeeName}");
    });

    // optional parameter indicater ? + if clause
    // id is a route constriant to restrict the id as integer 
    endpoints.Map("product/details/{id:int:range(1,1000)?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int? productId = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product id - {productId}");
        } else
        {
            await context.Response.WriteAsync($"Product id is not supplied");
        }
    });

    // Eg: daily-digest-report/{reportdate}
    // DateTime constriant 
    endpoints.Map("daily-digest-report/{reportdate:datetime?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("reportdate"))
        {
            DateTime? reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
            await context.Response.WriteAsync($"In daily-digest-report -{reportDate}");
        }
        else
        {
            await context.Response.WriteAsync($"In daily-digest-report is not provided");
        }
    });

    // Eg: cities/cityid
    // Guid constriant 
    endpoints.Map("cities/{cityid:guid?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("cityid")){
            Guid? cityId =  Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
            await context.Response.WriteAsync($"cities guid is {cityId}");
        }else
        {
            await context.Response.WriteAsync($"cities guid is not provided");
        }
    });

    //sales-report/2023/month
    // months is the custom constraint defines in the CustomConstraints
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        
            int year = Convert.ToInt32(context.Request.RouteValues["year"]);
            string? month = Convert.ToString(context.Request.RouteValues["month"]);
            
        
        if (month == "apr" || month == "july"|| month=="oct" || month == "jan")
        {
            await context.Response.WriteAsync($"sales report - {year} - {month}");
        } else
        {
           await context.Response.WriteAsync($"sales report {month} is not avalialbe");
        }
       
    });

    //sales-report/2024/jan
    endpoints.Map("sales-report/2024/jan", async context =>
    {
        await context.Response.WriteAsync("sales report exclusively for 2024 - jan");
    });
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});
app.Run();
