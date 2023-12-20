using OpenQA.Selenium;
using SpecFlowProject.PageObjectModel;
using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace StoreAdmin_BDD.StepDefinitions
{
    [Binding]
    public class BentoneAppDefinitions : LoginPOM
    {

        [Given(@"The user is on the login page")]
        public void GivenTheUserIsOnTheLoginPage()
        {
            loginPOM();
        }

        [When(@"User pass the username and password")]
        public void WhenUserPassTheUsernameAndPassword()
        {

        }

        [When(@"User click on login button")]
        public void WhenUserClickOnLoginButton()
        {

        }


        [Then(@"User naviagate to '([^']*)'")]
        public void ThenUserNaviagateTo(string name)
        {

            //Use implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            LoginPOM managerCareTeam = new LoginPOM();

            managerCareTeam.ManagerCareTeam( name);

                ClickByIndex();


            Validation();


            containerClick();

            clickonAward();


            awardCategoryClick();



            }

           

        }
    }
