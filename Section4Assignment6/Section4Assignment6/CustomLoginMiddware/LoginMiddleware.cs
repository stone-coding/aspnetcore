using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using System.Web;

namespace Section4Assignment6.CustomLoginMiddware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
                // read the http response body 
                StreamReader streamReader = new StreamReader(httpContext.Request.Body);
                string body = await streamReader.ReadToEndAsync();

                // parse the request body from string into Dictionary 
                Dictionary<string,StringValues> queryDict =  Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

            string? email = null, password = null;

            if (httpContext.Request.Path == "/" && httpContext.Request.Method == "POST")
            {
                // Below are post request
                // condition1: The submission of email and password will return a 200 


                // condition2: Either email or password is incorrect will return a 400

                // condition3: Neither email or password is submitted will return a 400

                // condition4: Password is not submitted will return a 400

                // condition5: Email is not submitted will return a 400

              
                // read first key value pair in the queryDict
                if (queryDict.ContainsKey("email"))
                {
                    email = Convert.ToString(queryDict["email"][0]);
                } else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for email\n");
                }

                // read second key value pair in the queryDict
                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]);
                }
                else
                {
                   
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid input for password\n");
                          
                }

              


                // if both email and password are submitted in the request
                if (string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(password) == false)
                {
                    string validEmail = "admin@example.com", validPassword = "admin1234";
                    bool isValidLogin;
                    if(email == validEmail && password == validPassword)
                    {
                        isValidLogin = true;
                    } else
                    {
                        isValidLogin= false;
                    }

                    // send response
                    if(isValidLogin)
                    {
                        await httpContext.Response.WriteAsync("Successful login");
                    } else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid login");
                    }

                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for email\n");
                    await httpContext.Response.WriteAsync("Invalid input for password\n");
                }

  



                } // end of post 
              // Below are get request:
              // condition1: return 200 
              else
            {
                await _next(httpContext);
            }

            

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
