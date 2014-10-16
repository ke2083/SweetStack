using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public abstract class CommandBase
    {
        public virtual ICollection<Ln> Script { get; set; }
        public abstract bool Callback { get; set; }
        public string Name { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the CommandBase class.
        /// </summary>
        public CommandBase(string name)
        {
            Script = new List<Ln>();
            Name = name;
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            foreach (var s in Script)
            {
                b.Append(s.ToString());
            }

            return b.ToString();
        }
    }
}
