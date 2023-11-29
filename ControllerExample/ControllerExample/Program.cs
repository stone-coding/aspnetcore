using ControllerExample.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();//adds all the controllers classes as services c# will automatically detect all control 1 line is enough;
var app = builder.Build();

//MapControllers will automatically calls UseRouting and UseEndpoints 
app.MapControllers();

/*app.UseRouting();
app.UseRouting().UseEndpoints(endpoints =>
{
    //MapControllers will pick up all the methods at a time and add them to the routers. 
    endpoints.MapControllers();
});*/

app.Run();
