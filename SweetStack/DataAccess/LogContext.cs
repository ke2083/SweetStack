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
        public DbSet<SweetTest> Tests { get; set; }
        public DbSet<TestLog> Messages { get; set; }
    }
}