using SweetStack.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SweetStack.DataAccess
{
    public class LogContext : DbContext
    {
        public DbSet<SweetTest> SweetTests { get; set; }
        public DbSet<TestInstance> TestRuns { get; set; }
    }
}