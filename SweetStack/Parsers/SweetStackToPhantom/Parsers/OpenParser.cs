using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class OpenParser
    {
        private const string jQuery = "http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js";

        public static Command Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(("->").ToCharArray());
            if (components.Length != 3)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Open command should be in format 'open -> url'", line));
                return null;
            }

            var url = components[2];

            return new Command("open")
            {
                Line = Js.Ln().Append(Js.Fn("page.open").Arg(Js.Str(url.Clean()))),
                Callback = true
            };

        }
    }
}
