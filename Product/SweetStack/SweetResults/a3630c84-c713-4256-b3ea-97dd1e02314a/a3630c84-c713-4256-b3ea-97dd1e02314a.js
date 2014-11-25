var webPage = require('webpage');var page = webPage.create();page.open('http://uat1.travel.saga.co.uk',function(){console.log('Taking screenshot...');page.render('1.png');phantom.exit();});
