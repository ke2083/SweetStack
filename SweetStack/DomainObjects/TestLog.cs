using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class TestLog
    {
        [Key]
        public Guid Id { get; set; }

        public string Message { get; set; }

    }
}
