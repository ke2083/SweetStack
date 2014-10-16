using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetStack.DomainObjects
{
    public class ParseResult
    {
        public IList<string> Errors { get; set; }
        public string Phantom { get; set; }
        public bool Success { get; set; }
        public ParseResult()
        {
            Success = true;
            Errors = new List<string>();
        }
    }
}
