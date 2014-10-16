using SweetStack.DataAccess;
using SweetStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                foreach (var log in test.Messages)
                { 
                    
                
                }

                return View(model);
            }
        }
    }
}