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
    public class PhantomLogger : IDisposable
    {

        private readonly LogContext context;
        private readonly Process process;
        private readonly string name;
        private readonly DateTime startTime;

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
            if (process != null)
                process.Dispose();
        }

        public PhantomLogger(Process p, string processName, string sweetStackCode)
        {
            process = p;
            name = processName;
            startTime = DateTime.Now;
            context = new LogContext();
            context.Tests.Add(new SweetTest
            {
                Name = processName,
                Timestamp = DateTime.Now,
                SweetStackCode = sweetStackCode
            });
            context.SaveChanges();
        }

        private void SaveLog(string message, bool completed)
        {
            var test = context.Tests.Find(name);           
            test.Completed = completed;
            var msg = new TestLog
            {
                Id = Guid.NewGuid(),
                Message = message,
                Test = test.Name
            };

            context.Messages.Add(msg);
            context.SaveChanges();
        }

        public void Log()
        {
            var end = false;
            while (!end)
            {
                var log = string.Empty;
                if (process.HasExited || !process.Responding || DateTime.Now.CompareTo(startTime.AddMinutes(20)) > 0)
                {
                    log = process.StandardOutput.ReadToEnd();
                    if (string.IsNullOrEmpty(log)) log = process.StandardError.ReadToEnd();
                    if (!process.HasExited && !process.Responding) process.Kill();

                    SaveLog(log, true);
                    process.Dispose();
                    end = true;
                    return;
                }

                if (process.StandardOutput.Peek() > -1)
                {
                    log = process.StandardOutput.ReadToEnd();
                    process.StandardOutput.DiscardBufferedData();
                    SaveLog(log, false);
                }

                Thread.Sleep(100);
            }
        }
    }
}