using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading;
using SweetStack.DataAccess;
using SweetStack.DomainObjects;

namespace SweetStack.BusinessLogic
{
    public static class SweetTestRunner
    {

        public static ParseResult Execute(string name, string sweetStackCode, string siteRoot, Guid sweetTestId)
        {
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var code = parser.ParseToPhantom(sweetStackCode.Split(Environment.NewLine.ToCharArray()).Where(s=>!string.IsNullOrEmpty(s)).ToList());
            if (!code.Success) return code;

            var testRunId = Guid.NewGuid();
            System.IO.Directory.CreateDirectory(string.Format("{0}\\{1}", siteRoot, testRunId));
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
            return code;
        }

    }
}
