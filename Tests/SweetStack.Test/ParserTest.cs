using NUnit.Framework;
using SweetStack.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetStack.Test
{
    [TestFixture]
    public class ParserTest
    {

        [Test]
        public void TestOpenCommand()
        {
            var command = "open -> http://www.bing.com";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();page.open('http://www.bing.com');";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestClickCommand()
        {
            var command = "click -> #myBtn";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();page.evaluate(function(){$('#myBtn').trigger('click');});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestProofCommand()
        {
            var command = "proof -> homepage.png";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();page.render('homepage.png');";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestTypeCommand()
        {
            var command = "type #txtBox -> Kaylee Eluvian";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();page.evaluate(function(){$('#txtBox').val('Kaylee Eluvian');});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestValueEqualsCommand()
        {
            var command = "value #txtBox => Kaylee Eluvian";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();console.log(\"Testing '#txtBox' value is equal to 'Kaylee Eluvian'\");page.evaluate(function(){return $('#txtBox').val() === 'Kaylee Eluvian';});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestValueContainsCommand()
        {
            var command = "value #txtBox ~> Kaylee Eluvian";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();console.log(\"Testing '#txtBox' value contains 'Kaylee Eluvian'\");page.evaluate(function(){return $('#txtBox').val().search('Kaylee Eluvian') > -1;});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestValueNotEqualsCommand()
        {
            var command = "value #txtBox !> Kaylee Eluvian";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();console.log(\"Testing '#txtBox' value is not equal to 'Kaylee Eluvian'\");page.evaluate(function(){return $('#txtBox').val() !== 'Kaylee Eluvian';});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TestWaitCommand()
        {
            var command = "wait -> 5";
            var command2 = "proof -> home.png";
            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();setTimeout(function(){page.render('home.png');},5000);";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(new List<string> { command, command2 });
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }


        [Test]
        public void TestChoseCommand()
        {
            var element = "chose -> #myRadio";
            var expected = "var webPage = require('webpage');var page = webPage.create();console.log(\"Testing whether '#myRadio' is selected...\");var selected = page.evaluate(function(){return $('#myRadio').is(':checked');});if (selected === true) console.log('PASS');else console.log('FAIL');";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actual = parser.ParseToPhantom(new List<string> { element });
            Assert.That(actual.Success, Is.True);
            Assert.That(actual.Phantom, Is.EqualTo(expected));
        }

        [Test]
        public void TestFullStack()
        {
            var commands = new List<string> { 
                "open -> http://www.bing.com",
                "proof -> bing.png",
                "type input.b_searchbox -> javascript",
                "click -> #sb_form_go",
                "wait -> 1",
                "text -> 17,800,000",
                "proof -> bing_search.png"
            };

            var expectedOutput = "var webPage = require('webpage');var page = webPage.create();page.open('http://www.bing.com',function(){page.includeJs('http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js',function(){page.render('bing.png');page.evaluate(function(){$('input.b_searchbox').val('javascript');});page.evaluate(function(){$('#sb_form_go').trigger('click');});setTimeout(function(){setTimeout(function(){page.render('bing_search.png');phantom.exit();},1000);},1);});});";
            var parser = new SweetStack.Parsers.SweetStackToPhantom.SweetStack();
            var actualOutput = parser.ParseToPhantom(commands);
            Assert.That(actualOutput.Success, Is.True);
            Assert.That(actualOutput.Phantom, Is.EqualTo(expectedOutput));
        }

    }
}
