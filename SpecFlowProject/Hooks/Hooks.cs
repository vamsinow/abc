using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject.Utility;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
[assembly: Parallelizable(ParallelScope.Fixtures)]


namespace SpecFlowProject.Hook
{
    [Binding]
    [TestFixture]

    public class Hooks : ControlHelper
    {

        public object scenarioContext;

        [BeforeTestRun]
        public static void BeforeTestRun()

        {


            Console.WriteLine("Running before test run...");
            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
            ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext Context)
        {
            Console.WriteLine("Running before feature...");
            _feature = _extentReports.CreateTest<Feature>(Context.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("Running after feature...");
        }

        //[BeforeScenario("@Testers")]
        //public void BeforeScenarioWithTag()
        //{
        //    Console.WriteLine("Running inside tagged hooks in specflow");
        //}

        [BeforeScenario]
        [Retry(2)]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running before scenario...");
            InitializeDriver("edge");

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (_extentReports != null)
            {
                _extentReports.Flush();

                // Get the source path of the Extent Report HTML file
                string sourcePath = Path.Combine(testResultPath);

                // Get the destination folder path
                string destinationFolder = dir.Replace("bin\\Debug\\net6.0", "Extent Reports");

                // Create the destination folder if it doesn't exist
                Directory.CreateDirectory(destinationFolder);

                // Combine the destination folder path with the folder name
                string destinationPath = Path.Combine(destinationFolder, subfolder);


                try
                {
                    // Copy the entire folder to the destination
                    CopyFolder(sourcePath, destinationPath);

                    Console.WriteLine($"Extent Report folder copied to: {destinationPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error copying Extent Report folder: {ex.Message}");
                }
            }

            // ...

            // Add the CopyFolder method to your Hooks class
             void CopyFolder(string sourceFolder, string destinationFolder)
            {
                try
                {
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    // Copy all files
                    foreach (string filePath in Directory.GetFiles(sourceFolder))
                    {
                        string fileName = Path.GetFileName(filePath);
                        string destinationFilePath = Path.Combine(destinationFolder, fileName);
                        File.Copy(filePath, destinationFilePath, true);
                    }

                    // Copy all subdirectories
                    foreach (string subdirectory in Directory.GetDirectories(sourceFolder))
                    {
                        string subdirectoryName = Path.GetFileName(subdirectory);
                        string destinationSubdirectory = Path.Combine(destinationFolder, subdirectoryName);
                        CopyFolder(subdirectory, destinationSubdirectory);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error copying folder: {ex.Message}");
                }
            }

            Console.WriteLine("Running after scenario...");
            if (_driver != null)
            {
            //    _driver.Quit();

            }
           

        }




        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            // When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName);
                }
            }

            // When scenario fails
            if (scenarioContext.TestError != null)
            {
                if (stepType == "Given" || stepType == "When" || stepType == "Then" || stepType == "And")
                {
                    // Capture screenshot and attach to the Extent Report
                    _scenario.CreateNode<Given>(stepName).Fail(
                        scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(AddScreenshot(_driver, scenarioContext)).Build()
                    );



              //    _driver.Close();
              //    _driver.Quit();
                }
            }
        }

        public string AddScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            try
            {
                ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
                Screenshot screenshot = takesScreenshot.GetScreenshot();

                // Generate a modified name for the screenshot file based on scenario name and timestamp
                string modifiedFileName = $"{scenarioContext.ScenarioInfo.Title.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                // Combine the destination folder path with the modified file name
                string screenshotLocation = Path.Combine(testResultPath, modifiedFileName);

                screenshot.SaveAsFile(screenshotLocation, ScreenshotImageFormat.Png);

                Console.WriteLine($"Screenshot saved: {screenshotLocation}");
                return screenshotLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddScreenshot: {ex.Message}");
                return string.Empty;
            }

        }
    }
}