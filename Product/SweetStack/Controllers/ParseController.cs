using SweetStack.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SweetStack.Controllers
{
    public class ParseController : Controller
    {
        private static SweetStack.DomainObjects.ParseResult ParseCode(string sweetStackCode)
        {
            var trimmedCommands = sweetStackCode.Split(Environment.NewLine.ToCharArray()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var parseResults = parser.ParseToPhantom(trimmedCommands);
            return parseResults;
        }
        [HttpPost]
        public ActionResult Validate(string sweetStackCode)
        {
            var parseResults = ParseCode(sweetStackCode);
            parseResults.Phantom = string.Empty; // Reduce the size of the response to the client.
            return Json(parseResults);
        }

        [HttpPost]
        public ActionResult Run(string name, string sweetStackCode)
        {
            var results = SweetTestRunner.Execute(name, sweetStackCode, Server.MapPath("~/SweetResults"), Guid.NewGuid());
            Thread.Sleep(500);
            return Json(results);
        }
    }
}