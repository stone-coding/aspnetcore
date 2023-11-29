using System.Text.RegularExpressions;

namespace Routing.CustomConstraints
{
    public class MonthsCustomConstraints : IRouteConstraint
    {
        /*
         * httpcontext with request and response
         * route is the url to be directed to 
         * routkey is the fix literal texts in url
         * RouteValueDictionary is the parameter in url
         */
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // check whether the value exists
            if(!values.ContainsKey(routeKey))
            {
                return false;
            }

            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            string? monthValue = Convert.ToString(values[routeKey]);

            if (regex.IsMatch(monthValue))
            {
                return true;
            }
            return false;
        }
    }
}
