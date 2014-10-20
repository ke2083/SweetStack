using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class SweetTest
    {
        [Key]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Completed { get; set; }
        public string SweetStackCode { get; set; }
  
    }
}
