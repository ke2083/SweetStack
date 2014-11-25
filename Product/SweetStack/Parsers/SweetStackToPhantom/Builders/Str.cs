using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Builders
{
    public class Str : Statement
    {
        private readonly string _inline;
        /// <summary>
        /// Initializes a new instance of the Str class.
        /// </summary>
        public Str(string inlineStatement)
        {
            _inline = inlineStatement;
        }

        public override string ToString()
        {
            return _inline;
        }
    }
}
