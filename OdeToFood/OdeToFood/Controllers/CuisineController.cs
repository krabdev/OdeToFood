using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class CuisineController : Controller
    {
        //
        // GET: /Cuisine/

        //since .net 4.0 function parameters can have optional values, so French it's just an optional value
        public ActionResult Search(string name = "French")
        {
            //HtmlEncode will just encode it for malicius users
            var message = Server.HtmlEncode(name);

            //return Content(message);
            return Content(message);
        }

       
    }
}
