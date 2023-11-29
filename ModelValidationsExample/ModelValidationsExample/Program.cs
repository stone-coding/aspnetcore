using ModelValidationsExample.CustomModelBinders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//0 means add the PersonModelProvider in the 1st place when checking for its correspond Binder(PersonModelBinder)
builder.Services.AddControllers(options =>
{
    //options.ModelBinderProviders.Insert(0, new PersonModelProvider());// since test default binder disable the custom modelbinders for a while...
});


// allows model get/post the xml data form 
builder.Services.AddControllers().AddXmlSerializerFormatters();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
