using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")] //IActionResult is the parent class for all result...
        // url: bookstore?bookid=10?isloggedin=true
        public IActionResult Index()
        
        {

            // book id should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                /*Response.StatusCode = 400;
                return Content("Book id is not supplied");*/
                return BadRequest("Book id is not supplied");
            }

            //Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                /*Response.StatusCode = 400;
                return Content("Book id can not be null or empty");*/
                return BadRequest("Book id can not be null or empty");
            }

            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if(bookId<= 0)
            {
                /*        Response.StatusCode = 400;
                        return Content("Book id can't be less than 0 or equal to 0");*/
                // not found result -> not error
                // not found object reuslt -> with error 
                return BadRequest("Book id can't be less than 0 or equal to 0");
            }
            if (bookId > 1000)
            {
                /*Response.StatusCode = 400;
                return Content("Book id can't be greater than 1000");*/
                return NotFound("Book id can't be greater than 1000");
            }

            //isLoggedin should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                /*Response.StatusCode = 401;
                return Content("User must be authenticated ");*/
                return Unauthorized("User must be authenticated");
            }

            // Books is the action name after IActionResult.
            // Store is the controller name in the HomeController 
            // since the route is automatically passed to the controll so no need to pass the routeValue 
            
            //return new RedirectToActionResult("Books", "Store", new { });//302 - Found
            //return RedirectToAction("Book","Store", new {id = bookId});// shortcut for above 302 found


            //return new RedirectToActionResult("Books", "Store", new { },permanent:true);//301 - Permanently removed
            return RedirectToActionPermanent("Books", "Store", new {id= bookId});// shortcut for above 301 found

            
        }



    }
}
