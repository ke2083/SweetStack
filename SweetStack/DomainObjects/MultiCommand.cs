using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class MultiCommand : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the MultiCommand class.
        /// </summary>
        public MultiCommand(string name) : base(name)
        {
        }

        public override bool Callback
        {
            get {
                return false;
            }
            set
            {

            }
        }
    }
}
