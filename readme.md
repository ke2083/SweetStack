SweetStack
==========

SweetStack is a human-language front-end testing language that transcompiles into [PhantomJS](http://phantomjs.org/).  It's currently available as an Alpha .Net MVC project running on the Microsoft stack.

SweetStack runs server-side and allows a user to input their test code and run in via PhantomJS.

Screenshots
-----------
![The home page](https://raw.githubusercontent.com/ke2083/SweetStack/master/SweetStack/imgs/home.png])

![The results page](https://raw.githubusercontent.com/ke2083/SweetStack/master/SweetStack/imgs/testRuns.png)

Roadmap
-------

* Improved UI
* Improved commandset/bugfixes
* TrifleJS integration for Internet Explorer testing
* SlimerJS integration for Firefox testing

Syntax
------

Example:

	open -> http://www.bing.com
	proof -> bing.png
	type input.b_searchbox -> javascript
	click -> #sb_form_go
	wait -> 1
	text -> 18,000,000 results
	proof -> bing_search.png

### Commands

`size`

Simulate a device (default is desktop) 

	size -> smartphone
	size -> phablet
	size -> minitablet
	size -> tablet
	size -> desktop

`open`

Go to a url 

	open -> http://www.bing.com

`click`

Click on the matching element

	click -> #myBtn

`wait`

Pause for seconds 

	wait -> 2

`type`

Type keys to the matching element 

	type .myTxtBox -> This is my test input

`proof`

Takes a screenshot 

	proof -> homepage.png

`->`

Specifies what follows as a string of text 

	type #txtBox -> Kaylee Eluvian

`#`

Comment 

	# This is a comment

### Tests

`text`

Checks that the specified text exists on the page 

	text -> This text should appear

`value`

Gets the value of the specified element 

Checks that a value exactly equals a string 

	type #txtBox -> Kaylee Eluvian 
	value #txtBox => Kaylee Eluvian

Checks that a value contains a string 

	type #txtBox -> Kaylee Eluvian 
	value #txtBox ~> Eluvian

Checks that a value does not contain a string type #txtBox -> Kaylee Eluvian 

	value #txtBox !> Kalyee

`chose`

Gets whether the specified element is checked/selected 

	chose -> #myChkBox

Status
------

This is currently Alpha release- so unless you want to develop it, it probably isn't ready for you yet :) Any help is appreciated is bringing this out!

Running requirements
--------------------

* MVC.Net 5
* IIS
* SQL Server/Express
* NodeJS

Building
--------

* Check out the code
* Open and compile in Visual Studio 2013
* Open a node prompt and type: `npm install
* Then type `bower install

You're ready to go :)