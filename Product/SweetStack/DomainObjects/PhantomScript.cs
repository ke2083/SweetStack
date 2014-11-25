using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class PhantomScript : List<CommandBase>
    {
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var command in this.ToList())
            {
                result.Append(command.ToString());
            }

            return result.ToString();
        }
    }
}
