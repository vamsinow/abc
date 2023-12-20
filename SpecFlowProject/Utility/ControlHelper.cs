using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SpecFlowProject.Hook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using BoDi;
using System.ComponentModel;


namespace SpecFlowProject.Utility
{

    public class ControlHelper : ExtentReport
    {

        [ThreadStatic]
        public static IWebDriver _driver;
        
        //public static UserCredentials capabilities;
        //code 1


        public static Dictionary<string, string> data = null!;



        //public static ThreadLocal<IWebDriver> _driverThreadLocal = new ThreadLocal<IWebDriver>();
        //public static IWebDriver _driver => _driverThreadLocal.Value;



        //code2
        //public static IWebDriver _driver
        //{
        //    get { return _driverThreadLocal.Value; }
        //}
       

        public void InitializeDriver(string browser)
        {
            //switch (browser.ToLower())
            //{
            //    case "chrome":
            //        _driver = new ChromeDriver();
            //        break;
            //    case "firefox":
            //        _driver = new FirefoxDriver();
            //        break;
            //    // Add more cases for other browsers if needed
            //    case "edge":
            //        _driver = new EdgeDriver();
            //        break;
            //    default:
            //        throw new NotSupportedException($"Unsupported browser: {browser}");
            //}
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://devstoreadminweb.beltoneapps.com/";
        }





        public static void PressElement(By Locator)
        {
             _driver.FindElement(Locator).Click();
        
        
        
        }
        public static void EnterText(By locator, string text)
        {
            IWebElement inputField = _driver.FindElement(locator);
            inputField.SendKeys(text);
        }





        public static void ClickButton(By locator)
        {
            IWebElement button = _driver.FindElement(locator);
            if (button is IJavaScriptExecutor)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button);
            }
            else
            {
                button.Click();
            }
        }






        //  JAVA-SCRIPT EXECUTOR CODE

        public static void jsEnterText(By locator, string text)
        {
            IWebElement inputField = _driver.FindElement(locator);
            if (inputField is IJavaScriptExecutor)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", inputField, text);
            }
            else
            {
                inputField.SendKeys(text);
            }
        }





        public static void ScrollToElement(By locator)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            IWebElement element = _driver.FindElement(locator);
            if (element is IJavaScriptExecutor)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
        }

       
        public static void WhenTheUserEntersValidCredentialsFromJson()
        {
           string jsonFilePath = "C:\\Users\\Iray Trust\\Desktop\\VsCode\\VSCODE\\SpecFlowProjectSol\\SpecFlowProject\\StoreAdmin_BDD.json";
           string json = File.ReadAllText(jsonFilePath);


            
         data =  new Dictionary<string, string>( JsonSerializer.Deserialize<Dictionary<string, string>>(json));





            // Deserialize JSON into Credentials object
            // UserCredentials capabilities = JsonConvert.DeserializeObject<UserCredentials>(json);
            // EnterText(username1, data["username"]);

            Console.WriteLine(data["username"]);



        }
        

        public static void EnterCredentials(string username, string password)
        {
            // Implement the logic to enter credentials in your application
            // This might involve finding and interacting with login form elements
            // For example, assuming you have a method to type into username and password fields:
            // TypeIntoUsernameField(username);
            // TypeIntoPasswordField(password);
        }




        public class UserCredentials : ControlHelper
        {
            public string username { get; set; }
            public string password { get; set; }
        }



    }
}