using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.CustomModelBinders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    public class HomeController : Controller
    {
        // Bind: only bind the nameof property and omit other properties in the Models classes.
         /*[Bind(nameof(Person.personName), nameof(Person.Email),
        nameof(Person.Password),nameof(Person.ConfirmPassword))]*/
        [Route("register")]
        //FromBody allows you to receive the json data from the body of postman 
        //After adding the PersonModelProvider, [FromBody][ModelBinder(BinderType =typeof(PersonModelBinder))] can be omitted
        public IActionResult Index(Person person, [FromHeader(Name ="User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            {
                /*  List<string> errorList =  new List<string>();
                  foreach (var value in ModelState.Values) {

                      foreach (var error in value.Errors) {
                          errorList.Add(error.ErrorMessage);
                      }

                  }
                  string errors = string.Join("\n", errorList);*/
                // Select and SelectMany are replacable items for for-each loops
                // for more info click here: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/projection-operations
                string errors = string.Join("\n",ModelState.Values.SelectMany(value => value.Errors).Select(error=>error.ErrorMessage));
                return BadRequest(errors);
            }
            return Content($"{person}, {UserAgent}");
        }
    }
}
