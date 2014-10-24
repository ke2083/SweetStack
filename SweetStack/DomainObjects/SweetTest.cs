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
        /// <summary>
        /// Initializes a new instance of the SweetTest class.
        /// </summary>
        public SweetTest()
        {
            Instances = new List<TestInstance>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public string SweetStackCode { get; set; }
        public virtual List<TestInstance> Instances { get; set; }
  
    }
}
