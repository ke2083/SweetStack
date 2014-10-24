using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class TestInstance
    {
        /// <summary>
        /// Initializes a new instance of the TestInstance class.
        /// </summary>
        public TestInstance()
        {
            Messages = new List<TestLog>();
        }
        public bool Completed { get; set; }
        [Key]
        public Guid Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Ended { get; set; }
        public virtual SweetTest SweetTest { get; set; }
        public virtual List<TestLog> Messages { get; set; }
    }
}
