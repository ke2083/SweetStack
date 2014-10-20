using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class ViewportParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(("->").ToCharArray()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: size command should be in format 'size -> device'", line));
                return null;
            }

            var element = components[1].Trim();
            var height = 1024;
            var width = 1280;

            if (element == "smartphone") {
                width = 320;
                height = 480;
            }
            else if (element == "phablet")
            {
                width = 480;
                height = 720;
            }
            else if (element == "minitablet")
            {
                width = 720;
                height = 1024;
            }
            else if (element == "tablet")
            {
                width = 1024;
                height = 1280;
            }

            return new MultiCommand("size")
            {
                Script = new List<Ln>{
                        Js.Ln().Append(String.Format("console.log(\"Setting size to {0}...\")", element.Clean())),
                        Js.Ln().Append(string.Format("page.viewportSize = {{ width: {0}, height: {1} }}", width, height))
                    }
            };
        }
    }
}
