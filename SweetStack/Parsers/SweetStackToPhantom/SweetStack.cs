using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using SweetStack.Parsers.SweetStackToPhantom.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom
{
    public class SweetStack
    {
        private readonly IDictionary<string, Func<string, int, ParseResult, CommandBase>> Parsers;

        /// <summary>
        /// Initializes a new instance of the SweetStack class.
        /// </summary>
        public SweetStack()
        {
            Parsers = new Dictionary<string, Func<string, int, ParseResult, CommandBase>>();
            Parsers.Add("open", OpenParser.Parse);
            Parsers.Add("click", ClickParser.Parse);
            Parsers.Add("proof", ProofParser.Parse);
            Parsers.Add("type", TypeParser.Parse);
            Parsers.Add("value", ValueParser.Parse);
            Parsers.Add("wait", WaitParser.Parse);
            Parsers.Add("chose", ChoseParser.Parse);
            Parsers.Add("text", TextParser.Parse);
        }

        public ParseResult ParseToPhantom(IList<string> commands)
        {
            var script = new List<CommandBase>();
            var result = new ParseResult();
            script.Add(new Command("include") { Line = Js.Ln().Append("var webPage = require('webpage')") });
            script.Add(new Command("include") { Line = Js.Ln().Append("var page = webPage.create();") });
            var lineNumber = 0;
            foreach (var command in commands.Where(c => !c.StartsWith("#")))
            {
                lineNumber += 1;
                var commandName = command.Substring(0, 5).Trim();
                if (!Parsers.ContainsKey(commandName))
                {
                    result.Success = false;
                    result.Errors.Add(string.Format("Line {0}: Unrecognised command", lineNumber));
                    continue;
                }

                script.Add(Parsers[commandName](command, lineNumber, result));
            }

            script = script.Where(s => s != null).ToList();
            script.Add(new Command("finish") { Line = Js.Ln().Append("phantom.exit()") });

            // Inject jQuery
            var positionOfOpen = script.FindIndex(c => c.Name == "open");
            if (positionOfOpen > -1) script.Insert(positionOfOpen + 1, new Command("include") { Line = Js.Ln().Append(Js.Fn("page.includeJs").Arg("'http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js'")), Callback = true });

            // Wrap all timeouts in an extra timeout (bug with injectJs in Phantom)
            var wrappedTimeouts = new List<CommandBase>();
            foreach (var c in script)
            {
                if (c.Name == "wait")
                {
                    wrappedTimeouts.Add(new Command("wait") { Line = Js.Ln().Append(Js.Fn("setTimeout").Arg("1")), Callback=true });
                }

                wrappedTimeouts.Add(c);
            }

            script = wrappedTimeouts;
            script.Reverse();
            var assembledScript = new PhantomScript();
            foreach (var command in script)
            {
                if (!command.Callback)
                {
                    assembledScript.Insert(0, command);
                }
                else
                {
                    var temp = new StringBuilder();
                    // Take what we have so far and put it as a callback to this new command
                    foreach (var prevScript in assembledScript)
                    {
                        
                        foreach (var prevLine in prevScript.Script)
                        {
                            temp.Append(prevLine.ToString());
                        }
                    }

                    if (command.Name == "open" || command.Name == "include") (command as Command).Line.Fn().InsertArg(Js.Fn("function").Ln(Js.Ln().Append(temp.ToString())), 1);
                    else (command as Command).Line.Fn().InsertArg(Js.Fn("function").Ln(Js.Ln().Append(temp.ToString())), 0);
                    assembledScript = new PhantomScript { command };
                }
            }

            result.Phantom = assembledScript.ToString();
            return result;
        }
    }
}