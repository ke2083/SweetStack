using NUnit.Framework;
using SweetStack.DomainObjects;
using SweetStack.Parsers.SweetStackToPhantom.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetStack.Test
{
    [TestFixture]
    public class BuilderTest
    {

        [Test]
        public void TestBuildsClickFunction()
        {
            var element = "#myBtn";
            var expected = "page.evaluate(function(){$('#myBtn').trigger('click');});";
            var actual = Js.Ln().Append(Js.Fn("page.evaluate")
                            .Arg(Js.Fn("function")
                                .Ln(
                                    Js.Ln().Append(
                                        Js.Str(string.Format("$('{0}').trigger('click')", element.Trim()))
                                    )
                                )
                            )).ToString();

            Assert.That(actual, Is.EqualTo(expected));

        }

    }
}
