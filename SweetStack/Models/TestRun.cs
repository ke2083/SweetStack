using SweetStack.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SweetStack.Models
{
    public class TestRun
    {
        public List<FormattedTestMessage> FormattedResults { get; set; }
        public Dictionary<string, string> Screenshots { get; set; }
        public DateTime Timestamp { get; set; }
        public string SweetStackCode { get; set; }
    }
}