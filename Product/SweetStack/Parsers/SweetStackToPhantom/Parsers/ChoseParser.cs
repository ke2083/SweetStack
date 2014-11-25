using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{

    public static class ChoseParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Chose command should be in format 'chose -> selector'", line));
                return null;
            }

            var element = components[1];

            return new MultiCommand("chose")
            {
                Script = new List<Ln>{
                    Js.Ln().Append(String.Format("console.log(\"Testing whether {0} is selected...\")", element.Clean())),
                    Js.Ln().Append("var selected = ").Append(Js.Fn("page.evaluate")
                            .Arg(Js.Fn("function")
                                .Ln(
                                    Js.Ln().Append(
                                        Js.Str(string.Format("return $({0}).is(':checked')", element.Clean()))
                                    )
                                )
                            )),
                    Js.Ln().Append("if (selected === true) console.log('PASS')"),
                    Js.Ln().Append("else console.log('FAIL')")
                }
            };
        }

    }
}
