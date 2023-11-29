using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using System.Security.Cryptography;
using ControllerExample.Models;

namespace ControllerExample.Controllers
{
    // specific class name + Controller
    public class HomeController:Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
/*            return new ContentResult()
            {
                Content =
                "Hello fron index",
                ContentType = "text/plain"
            }; */  
            
           // return Content("Hello from index", "text/plain");// response-body, content-type
            return Content("<h1>Welcome</h1> <h2>Hellofrom index<h2/>","text/html");                                                 
                                                             
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Smith",
                Age = 25,
            };
            // return new JsonResult(person);
            return Json(person);
            // return "{\"key\": \"value\"}";//json format return looks ugly 
        }

        //VirtualFileResult for download the file from wwwroot folder
        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/docs.pdf", "application/pdf");
            return File("/docs.pdf", "application/pdf");
        }

        //PhysicalFileResult for downloadthe file not in wwwroot like in a folder in the windows 10 system 
        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult(@"D:\c# samples\docs.pdf", "application/pdf");
            return PhysicalFile("/docs.pdf", "application/pdf");
        }

        //FileContentResult for download the file in online api, remote database, byte format... very useful  
        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"D:\c# samples\docs.pdf");
            //return new FileContentResult(bytes, "application/pdf");
            return File(bytes, "application/pdf");
        }

        //FileContentResult for download the file in online api, remote database, byte format... very useful  
        [Route("file-download4")]
        // IActionResult is the parent interface for all action result classes suches as ContentResult, JsonResult... 
        public IActionResult FileDownload4()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"D:\c# samples\docs.pdf");
            //return new FileContentResult(bytes, "application/pdf");
            return File(bytes, "application/pdf");
        }
    }
}
