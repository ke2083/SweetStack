using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class TestMessage
    {
        public enum StatusTypes
        {
            Pass,
            Fail,
            Information,
            Warning
        }

        public StatusTypes Status { get; set; }
        public string Message { get; set; }
    }
}
