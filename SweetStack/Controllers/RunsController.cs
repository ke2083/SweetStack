using SweetStack.DataAccess;
using SweetStack.DomainObjects;
using SweetStack.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SweetStack.Controllers
{
    public class RunsController : Controller
    {

        private readonly LogContext logContext;

        /// <summary>
        /// Initializes a new instance of the RunsController class.
        /// </summary>
        /// <param name="logContext"></param>
        public RunsController()
        {
            this.logContext = new LogContext();
        }

        // GET: Runs
        public ActionResult Index()
        {
            var tests = logContext.TestRuns.OrderByDescending(t => t.Started).ToList();
            return View(tests);
        }

        public ActionResult Details(Guid id)
        {
            var testRun = logContext.TestRuns.Find(id);
            var model = new TestRun();
            var messages = testRun.Messages.ToList();
            var formattedMessage = new List<FormattedTestMessage>();
            foreach (var log in messages)
            {
                var components = log.Message.Split(Environment.NewLine.ToCharArray());
                foreach (var c in components)
                {
                    if (string.IsNullOrEmpty(c)) continue;

                    if (c.StartsWith("PASS"))
                    {
                        formattedMessage.Add(new FormattedTestMessage { Message = c, Status = FormattedTestMessage.StatusTypes.Pass });
                    }
                    else if (c.StartsWith("FAIL"))
                    {
                        formattedMessage.Add(new FormattedTestMessage { Message = c, Status = FormattedTestMessage.StatusTypes.Fail });
                    }
                    else if (c.StartsWith(" "))
                    {
                        formattedMessage.Add(new FormattedTestMessage { Message = c, Status = FormattedTestMessage.StatusTypes.Warning });
                    }
                    else
                    {
                        formattedMessage.Add(new FormattedTestMessage { Message = c, Status = FormattedTestMessage.StatusTypes.Information });
                    }
                }
            }

            var dir = new System.IO.DirectoryInfo(string.Format("{0}\\{1}", Server.MapPath("~/Content"), testRun.Id));
            var pngs = dir.GetFiles("*.png");
            var jpgs = dir.GetFiles("*.jpg");
            var gifs = dir.GetFiles("*.gif");

            var allImages = new List<FileInfo>();
            allImages.AddRange(pngs);
            allImages.AddRange(jpgs);
            allImages.AddRange(gifs);

            var imageList = new Dictionary<string, string>();
            foreach (var img in allImages)
            {
                imageList.Add(img.Name, string.Format("/Content/{0}/{1}", testRun.Id, img.Name));
            }

            model.Screenshots = imageList.OrderBy(i => i.Key).ToDictionary(i => i.Key, i => i.Value);
            model.FormattedResults = formattedMessage;
            model.Timestamp = testRun.Started;
            model.SweetStackCode = testRun.SweetTest.SweetStackCode;
            return View(model);

        }
    }
}