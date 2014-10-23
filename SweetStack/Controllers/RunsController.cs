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
        // GET: Runs
        public ActionResult Index()
        {
            using (LogContext logContext = new LogContext())
            {
                var tests = logContext.Tests.OrderByDescending(t => t.Timestamp).ToList();
                return View(tests);
            }
        }

        public ActionResult Details(string id)
        {
            using (LogContext logContext = new LogContext())
            {
                var test = logContext.Tests.Find(id);
                var model = new TestRun();
                var messages = logContext.Messages.Where(m => m.Test == test.Name);
                var formattedMessage = new List<TestMessage>();
                foreach (var log in messages)
                {
                    var components = log.Message.Split(Environment.NewLine.ToCharArray());
                    foreach (var c in components)
                    {
                        if (string.IsNullOrEmpty(c)) continue;

                        if (c.StartsWith("PASS"))
                        {
                            formattedMessage.Add(new TestMessage { Message = c, Status = TestMessage.StatusTypes.Pass });
                        }
                        else if (c.StartsWith("FAIL"))
                        {
                            formattedMessage.Add(new TestMessage { Message = c, Status = TestMessage.StatusTypes.Fail });
                        }
                        else if (c.StartsWith(" "))
                        {
                            formattedMessage.Add(new TestMessage { Message = c, Status = TestMessage.StatusTypes.Warning });
                        }
                        else
                        {
                            formattedMessage.Add(new TestMessage { Message = c, Status = TestMessage.StatusTypes.Information });
                        }
                    }
                }

                var dir = new System.IO.DirectoryInfo(string.Format("{0}\\{1}", Server.MapPath("~/Content"), test.Name));
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
                    imageList.Add(img.Name, string.Format("/Content/{0}/{1}", test.Name, img.Name));
                }

                model.Screenshots = imageList.OrderBy(i => i.Key).ToDictionary(i=>i.Key, i=>i.Value);
                model.FormattedResults = formattedMessage;
                model.Timestamp = test.Timestamp;
                model.SweetStackCode = test.SweetStackCode;
                return View(model);
            }
        }
    }
}