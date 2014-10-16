using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class Command : CommandBase
    {

        public override bool Callback { get; set; }
        public Ln Line { get; set; }
        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        public Command(string name)
            : base(name)
        {
            
        }

        public override ICollection<Ln> Script
        {
            get
            {
                return new List<Ln> { Line };
            }
            set
            {
                
            }
        }
    }
}
