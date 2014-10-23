using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class TextParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Text command should be in format 'text -> text to find'", line));
                return null;
            }

            var element = components[1];

            return new MultiCommand("text")
            {
                Script = new List<Ln>{
                        Js.Ln().Append(String.Format("console.log(\"Testing whether {0} is in markup...\")", element.Clean())),
                        Js.Ln().Append("var found = ").Append(Js.Fn("page.evaluate")
                                .Arg(Js.Fn("function")
                                    .Ln(
                                        Js.Ln().Append(
                                            Js.Str(string.Format("return $('body').text().search({0}) > -1", element.Clean()))
                                        )
                                    )
                                )),
                        Js.Ln().Append("if (found === true) console.log('PASS')"),
                        Js.Ln().Append("else console.log('FAIL')")
                    }
            };
        }
    }
}
