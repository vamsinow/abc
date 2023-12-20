using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using SpecFlowProject.Hook;
using SpecFlowProject.PageObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        public static String dir = AppDomain.CurrentDomain.BaseDirectory;

        public static string subfolder = $"{DateTime.Now:dd_MM_yyyy_HH_mm_ss}";
        //  public static String testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResults");
        public static String testResultPath = dir.Replace("bin\\Debug\\net6.0", $"Extent Report/{subfolder}");
       
        public static ExtentTest step;

        public static void ExtentReportInit()
        {
            var htmlReporter = new ExtentHtmlReporter(testResultPath);
            htmlReporter.Config.ReportName = "Automation Status Report";
            htmlReporter.Config.DocumentTitle = "Automation Status Report";
            htmlReporter.Config.Theme = Theme.Standard;
            //   htmlReporter.Config.CSS = ".ScreenshotImageFormat.Png{width : 200%  }";
            htmlReporter.Start();

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "Lenskart");
            _extentReports.AddSystemInfo("Browser", "Chrome");
            _extentReports.AddSystemInfo("OS", "Windows");

        }
        public static void ExtentReportTearDown()
        {



            _extentReports.Flush();
        }

        
    }
}
    