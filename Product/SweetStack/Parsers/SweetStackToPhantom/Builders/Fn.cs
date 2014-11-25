using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Builders
{
    public class Fn : Statement
    {
        private readonly string _name;
        private readonly ICollection<Statement> Arguments;
        private readonly ICollection<Ln> Lines;

        /// <summary>
        /// Initializes a new instance of the Function class.
        /// </summary>
        public Fn(string name)
        {
            _name = name;
            Arguments = new List<Statement>();
            Lines = new List<Ln>();
        }

        public Fn Arg(Statement argument)
        {
            Arguments.Add(argument);
            return this;
        }

        public Fn Arg(string argument)
        {
            Arguments.Add(Js.Str(argument));
            return this;
        }

        public Fn InsertArg(string argument, int location)
        {
            (Arguments as List<Statement>).Insert(location, Js.Str(argument));
            return this;
        }

        public Fn InsertArg(Statement argument, int location)
        {
            (Arguments as List<Statement>).Insert(location, argument);
            return this;
        }

        public Fn Ln(Ln line)
        {
            Lines.Add(line);
            return this;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.Append(_name + "(");
            var argumentsList = string.Join(",", Arguments.Select(a => a.ToString()).ToArray());
            output.Append(argumentsList + ")");
            if (Lines.Any())
            {
                output.Append("{");
                Lines.ToList().ForEach(l => output.Append(l.ToString()));
                output.Append("}");
            }

            return output.ToString();
        }


    }
}