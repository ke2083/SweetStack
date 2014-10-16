using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Builders
{
    public class Ln
    {
        private readonly ICollection<Statement> statements;
        /// <summary>
        /// Initializes a new instance of the Ln class.
        /// </summary>
        /// <param name="line"></param>
        public Ln()
        {
            statements = new List<Statement>();
        }

        public Fn Fn()
        {
            return (statements.Where(s => s.GetType() == typeof(Fn))).Cast<Fn>().First();
        }

        public Ln Append(Statement function)
        {
            statements.Add(function);
            return this;
        }

        public Ln Append(string statement)
        {
            statements.Add(Js.Str(statement));
            return this;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            statements.ToList().ForEach(s => output.Append(s.ToString()));
            if (!output.ToString().EndsWith(";")) output.Append(";");
            return output.ToString();
        }
    }
}
