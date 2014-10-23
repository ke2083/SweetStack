using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class ClickParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var components = command.Split(("->").ToCharArray()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            if (components.Length != 2)
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Click command should be in format 'click -> selector'", line));
                return null;
            }

            var element = components[1];

            return new Command("click")
            {
                Line = Js.Ln().Append(Js.Fn("page.evaluate")
                            .Arg(Js.Fn("function")
                                .Ln(Js.Ln().Append("function eventFire(el, etype){  if (el.fireEvent) {    el.fireEvent('on' + etype);  } else {    var evObj = document.createEvent('Events');    evObj.initEvent(etype, true, false);    el.dispatchEvent(evObj);  }}"))
                                .Ln(
                                    Js.Ln().Append(
                                        Js.Str(string.Format("eventFire($({0})[0], 'click')", element.Clean()))
                                    )
                                )
                            ))
            };
        }
    }
}