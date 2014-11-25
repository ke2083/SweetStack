using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Parsers
{
    public static class ArgumentCleanser
    {
        public static string Clean(this string args)
        {
            return "'" + args.Replace("\"", "").Replace("'", "").Trim() + "'";
        }
    }
}
