using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class WaitParser
    {

        public static Command Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var i = 0;
            if (components.Length != 2 || !int.TryParse(components[1], out i))
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Wait command should be in format 'wait -> seconds'", line));
                return null;
            }

            return new Command("wait")
            {
                Line =  Js.Ln().Append(Js.Fn("setTimeout").Arg(i + "000")),
                Callback = true
            };
        }
    }
}
