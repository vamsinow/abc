using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlowProject.Utility;
using StoreAdmin_BDD.StepDefinitions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;

namespace SpecFlowProject.PageObjectModel
{
    public class LoginPOM : ControlHelper
    {

        //Xpath For Username by id
        static By username1             = By.Id("Username");
        //Xpath For password by id
        static By password1             = By.Id("Password");
        //Xpath For login button by xpath
        static By Login                 = By.XPath("//button[normalize-space(text())='Login']");
        //Xpath For dropdown button foe selecting doctors by xpath
        static By dropdown              = By.XPath("//input[@id='parentCompanyObj']/following-sibling::span");

        static ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//ul[@id='parentCompanyObj_options']/li"));
        static IWebElement element1;
        static By Award                 = By.XPath("//div[normalize-space(text())='Awards']");
        static By ClickOnAwardCatDrop   = By.XPath("(//span[@class='e-input-group-icon e-ddl-icon e-icons e-ddl-disable-icon'])[1]");
        static By awardFunctionsBentone = By.XPath("//li[normalize-space(text())='Beltone']");
        static By awardFunctionCustom   = By.XPath("//li[normalize-space(text())='Custom']");
        static By awardTypeDropDown     = By.XPath("(//span[@class='e-input-group-icon e-ddl-icon e-icons e-ddl-disable-icon'])[2]");
        static By dropdownName = By.XPath("//div[@class='e-ddl e-lib e-input-group e-control-container e-control-wrapper e-float-input e-valid-input  valid']/child::span");


        ControlHelper controlHelper = new ControlHelper();



        // UserCredentials capabilities = new UserCredentials(); // Initialize capabilities with appropriate values



        public void loginPOM()
        {
            // Use implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // ControlHelper anotherInstance = new ControlHelper();


            WhenTheUserEntersValidCredentialsFromJson();


            EnterText(username1, data["username"]);


            EnterText(password1, data["password"]);


            PressElement(Login);
            Thread.Sleep(8000);

            //      List<string> newTb = new List<string>(_driver.WindowHandles);

            //   Switch to the second window
            //    _driver.SwitchTo().Window(newTb[0]);
        }
        public void ManagerCareTeam(string name)
        {
            //Use implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                try
                {
                    // Try to interact with the element
                  IWebElement  element = _driver.FindElement(By.XPath("//span[normalize-space(text())='" + name + "']/parent::a/parent::li"));
                    element.Click();
                    break;  // If successful, exit the loop
                }
                catch (StaleElementReferenceException)
                {
                    // Increment attempts and retry
                    attempts++;
                    Thread.Sleep(6000);
                }
            }

        }









        public static void ClickByIndex()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            PressElement(dropdown);



            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//ul[@id='parentCompanyObj_options']/li"));

            // Check if there are any elements

            if (elements.Count > 0)
            {
                int maxIndex = elements.Count;
                Random random = new Random();

                // Generate a random index within the range [0, maxIndex)
                int randomIndex = random.Next(0, maxIndex);

                string xpath = $"//ul[@id='parentCompanyObj_options']/li[{randomIndex + 1}]";

                 element1 = _driver.FindElement(By.XPath(xpath));
                string text = element1.Text;
                Console.WriteLine(text);


                try
                {
                    element1.Click();

                    Console.WriteLine($"Clicked on element with random index: {randomIndex}");
                    // Add additional logic as needed after clicking the element
                }
                catch (NoSuchElementException)
                {
                    // Handle the case where the element with the random index is not found
                }

                
            }
            else
            {
                Console.WriteLine("No elements found.");
                // Handle the case where no elements are found
            }

        }

        public static void Validation()
        {
            Thread.Sleep(4000);

             IWebElement name1 = _driver.FindElement(By.XPath("//div/child::i/following-sibling::span"));


            string m = name1.Text;
            Console.WriteLine(m);

            
           
        }

        public static void containerClick()
        {


            Thread.Sleep(3000);


            ReadOnlyCollection<IWebElement> elements1 = _driver.FindElements(By.XPath("(//h2/parent::div/parent::div/parent::div)"));


            Console.WriteLine(elements1);
            // Check if there are any elements

            if (elements1.Count > 0)
            {
                int maxIndex1 = elements1.Count;
                Random random1 = new Random();

                // Generate a random index within the range [0, maxIndex)
                int randomIndex1 = random1.Next(0, maxIndex1);

                string xpath1 = $"(//h2/parent::div/parent::div/parent::div)[{randomIndex1 + 1}]";

             //   ScrollToElement(xpath1);

                IWebElement element2 = _driver.FindElement(By.XPath(xpath1));


                try
                {



                    

                    element2.Click();

                    Console.WriteLine($"Clicked on element with random index: {randomIndex1}");
                    // Add additional logic as needed after clicking the element
                }
                catch (NoSuchElementException)
                {
                    // Handle the case where the element with the random index is not found
                }



            }
            }

        private static void ScrollToElement(string xpath1)
        {
            throw new NotImplementedException();
        }






        public static void clickonAward()
        {

            PressElement(Award);


            Thread.Sleep(4000);
        }
        public static void awardCategoryClick()
        {
            Thread.Sleep(6000);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            PressElement(ClickOnAwardCatDrop);

            PressElement(awardFunctionsBentone);

            PressElement(awardTypeDropDown);

            PressElement(dropdownName);


        }


        public static void awardCatogoryFunctions()
        {



        }





        public class UserCredentials
        {
            public string username { get; set; }
            public string password { get; set; }
        }


    }
}

