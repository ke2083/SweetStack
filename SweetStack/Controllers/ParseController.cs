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
            var commands = sweetStackCode.Split(Environment.NewLine.ToCharArray()).ToList();
            var trimmedCommands = commands.Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();

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
            var code = ParseCode(sweetStackCode);
            if (!code.Success) return Json(code);

            var sweetTestId = Guid.NewGuid();
            var testRunId = Guid.NewGuid();
            var siteRoot = Server.MapPath("~/Content");
            System.IO.Directory.CreateDirectory(siteRoot + "\\" + testRunId);
            System.IO.File.WriteAllLines(string.Format("{1}\\{0}\\{0}.js", testRunId, siteRoot), new string[] { code.Phantom });
            var info = new ProcessStartInfo("c:\\phantomjs\\phantomjs.exe", string.Format("\"{1}\\{0}\\{0}.js\"", testRunId, siteRoot))
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = String.Format("{0}\\{1}", siteRoot, testRunId)
            };
            var p = Process.Start(info);
            var logger = new PhantomLogger(p, sweetTestId, testRunId, name, sweetStackCode);
            ThreadPool.QueueUserWorkItem((obj) => logger.Log());
            return null;

        }
    }
}