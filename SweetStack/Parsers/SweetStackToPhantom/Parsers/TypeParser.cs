using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class TypeParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Type command should be in format 'type selector -> input'", line));
                return null;
            }

            var element = components[0].Substring(4);
            var input = components[1];
            return new Command("type")
            {
                Line = Js.Ln().Append(
                        Js.Fn("page.evaluate")
                            .Arg(Js.Fn("function")
                                .Ln(
                                    Js.Ln().Append("function eventFire(el, etype){  if (el.fireEvent) {    el.fireEvent('on' + etype);  } else {    var evObj = document.createEvent('Events');    evObj.initEvent(etype, true, false);    el.dispatchEvent(evObj);  }}")
                                )
                                .Ln(
                                    Js.Ln().Append(string.Format("$({0}).val({1});", element.Clean(), input.Clean()))
                                )
                                .Ln(
                                    Js.Ln().Append(string.Format("eventFire($({0})[0], 'keydown')", element.Clean()))
                                )
                            )
                        )
            };
        }
    }
}
