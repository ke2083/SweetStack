using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class ProofParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Proof command should be in format 'Proof -> filename.png'", line));
                return null;
            }

            var file = components[1];
            return new MultiCommand("proof")
            {
                Script = new List<Ln>{
                    Js.Ln().Append("console.log('Taking screenshot...')"),
                    Js.Ln().Append(Js.Fn("page.render").Arg(Js.Str(file.Clean())))
                }
            };
        }
    }
}
