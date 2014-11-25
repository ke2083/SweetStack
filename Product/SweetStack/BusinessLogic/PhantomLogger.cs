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
        private readonly Guid sweetTestId;
        private readonly DateTime startTime;
        private readonly Guid testRunId;

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
            if (process != null)
                process.Dispose();
        }

        public PhantomLogger(Process p, Guid sweetTestId, Guid testRunId, string name, string sweetStackCode)
        {
            process = p;
            this.sweetTestId = sweetTestId;
            startTime = DateTime.Now;
            context = new LogContext();

            // Do we already have this test?
            if (!context.SweetTests.Any(t => t.Id == sweetTestId))
            {
                context.SweetTests.Add(new SweetTest
                {
                    Name = name,
                    Id = sweetTestId,
                    Timestamp = DateTime.Now,
                    SweetStackCode = sweetStackCode
                });
                context.SaveChanges();
            }

            var sweetTest = context.SweetTests.Find(sweetTestId);
            this.testRunId = testRunId;
            sweetTest.Instances.Add(new TestInstance()
            {
                Completed = false,
                Ended = null,
                Id = testRunId,
                Started = DateTime.Now,
                SweetTest = sweetTest
            });

            context.SaveChanges();
        }

        private void SaveLog(string message, bool completed)
        {
            var test = context.SweetTests.Find(sweetTestId);
            var run = test.Instances.Find(t=>t.Id == testRunId);
            run.Completed = completed;
            if (completed)
            {
                run.Ended = DateTime.Now;
            }

            var msg = new TestLog
            {
                Id = Guid.NewGuid(),
                Message = message,
                Test = test.Name,
                TestRun = run
            };

            run.Messages.Add(msg);
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