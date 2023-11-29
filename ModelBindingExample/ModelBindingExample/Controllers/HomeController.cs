using Microsoft.AspNetCore.Mvc;
using ModelBindingExample.Models;
namespace ModelBindingExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{bookid}/{isloggedin}")] //IActionResult is the parent class for all result...
        // url: bookstore?bookid=10?isloggedin=true
        // Add constriant [FromRoute] or [FromQuery] to restrict the parameters from url such as [FromRoute]int? bookid 
        // if model class is passed as parameter, model class get the value passed to controller class by route, query string
        // form-data is better than form-urlencoded in file attchment
        public IActionResult Index(int? bookid, [FromQuery]bool? isloggedin, Book book)
        {

            // book id should be applied
            if (bookid.HasValue == false)
            {
                /*Response.StatusCode = 400;
                return Content("Book id is not supplied");*/
                return BadRequest("Book id is not supplied");
            }

            //Book id can't be empty
            if (bookid.HasValue == false)
            {
                /*Response.StatusCode = 400;
                return Content("Book id can not be null or empty");*/
                return BadRequest("Book id can not be null or empty");
            }

            
            if(bookid <= 0)
            {
                /*        Response.StatusCode = 400;
                        return Content("Book id can't be less than 0 or equal to 0");*/
                // not found result -> not error
                // not found object reuslt -> with error 
                return BadRequest("Book id can't be less than 0 or equal to 0");
            }
            if (bookid > 1000)
            {
                /*Response.StatusCode = 400;
                return Content("Book id can't be greater than 1000");*/
                return NotFound("Book id can't be greater than 1000");
            }

            //isLoggedin should be true
            if (isloggedin == false)
            {
                /*Response.StatusCode = 401;
                return Content("User must be authenticated ");*/
                return Unauthorized("User must be authenticated");
            }

            return Content($"Book is: {bookid}, Book: {book}", "text/plain");

            
        }



    }
}
