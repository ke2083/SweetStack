using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SweetStack.Controllers
{
    public class HomeController : Controller
    {
        private readonly Parsers.SweetStackToPhantom.SweetStack parser;
        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        public HomeController()
        {
            parser = new Parsers.SweetStackToPhantom.SweetStack();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Parse(string code)
        {
            var commands = code.Split(Environment.NewLine.ToCharArray());
            var parsedData = parser.ParseToPhantom(commands);
            return Json(parsedData, JsonRequestBehavior.AllowGet);
        }
    }
}