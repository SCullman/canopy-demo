#I @".\packages\Selenium.WebDriver\lib\net40\"
#I @".\packages\canopy\lib\"

#r "WebDriver.dll"
#r "canopy.dll"

open canopy
open canopy.runner.classic
open canopy.classic

configuration.chromeDir  <- @"packages\Selenium.WebDriver.ChromeDriver\driver\win32"

start chrome

//define tests
"Login should fail for unkown users" &&& fun _ ->

  //arrange
  url "http://www.dnn-connect.org/login"
  "[id$=_txtUsername]" << "wrongUsername"
  "[id$=_txtPassword]" << "wrongPassword"
  //act
  click "[id$=_cmdLogin]"  
  //assert
  displayed "span:contains(Login Failed)"

let login site username password = 
  url (sprintf "%s/?ctl=login" site)
  press username
  press tab
  press password
  press enter

"Login should fail for unkown users with keyboard " &&& fun _ ->

  login "http://www.dnn-connect.org" "wrongUsername" "wrongPassword"
  //assert
  displayed "span:contains(Login Failed)"

//run your tests
run()