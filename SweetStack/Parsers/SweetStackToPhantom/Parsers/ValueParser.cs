using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class ValueParser
    {
        public static CommandBase Parse(string command, int line, ParseResult result)
        {
            var valueCommand = command.Substring(5);
            char[] splitter = new char[2];
            if (valueCommand.Contains("=>"))
                splitter = ("=>").ToCharArray();
            else if (valueCommand.Contains("~>"))
                splitter = ("~>").ToCharArray();
            else if (valueCommand.Contains("!>"))
                splitter = ("!>").ToCharArray();
            else
            {
                result.Success = false;
                result.Errors.Add(string.Format("Line {0}: Value command should be in format 'value selecter => text'", line));
                return null;
            }

            var components = valueCommand.Split(splitter).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            var element = components[0];
            var value = components[1];

            if (splitter[0] == '=')
                return new MultiCommand("value")
                {
                    Script = new List<Ln>{
                        Js.Ln().Append(string.Format("console.log(\"Testing {0} value is equal to {1}\");", element.Clean(), value.Clean())),
                        Js.Ln().Append(Js.Fn("page.evaluate").Arg(Js.Fn("function").Ln(Js.Ln().Append(string.Format("return $({0}).val() === {1}", element.Clean(), value.Clean())))))
                    }                   
                };
            else if (splitter[0] == '~')
                return new MultiCommand("value")
                {
                    Script = new List<Ln>{
                        Js.Ln().Append(string.Format("console.log(\"Testing {0} value contains {1}\");", element.Clean(), value.Clean())),
                        Js.Ln().Append(Js.Fn("page.evaluate").Arg(Js.Fn("function").Ln(Js.Ln().Append(string.Format("return $({0}).val().search({1}) > -1", element.Clean(), value.Clean())))))
                    }
                };
            else if (splitter[0] == '!')
                return new MultiCommand("value")
                {
                    Script = new List<Ln>{
                        Js.Ln().Append(string.Format("console.log(\"Testing {0} value is not equal to {1}\");", element.Clean(), value.Clean())),
                        Js.Ln().Append(Js.Fn("page.evaluate").Arg(Js.Fn("function").Ln(Js.Ln().Append(string.Format("return $({0}).val() !== {1}", element.Clean(), value.Clean())))))
                    }
                };
            else return null;
        }
    }
}
