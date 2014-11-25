using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SweetStack.Parsers.SweetStackToPhantom.Builders
{
    public static class Js
    {
        public static Fn Fn(string name) {
            return new Fn(name);
        }

        public static Ln Ln() {
            return new Ln();
        }

        public static Str Str(string value) {
            return new Str(value);
        }
    }
}
