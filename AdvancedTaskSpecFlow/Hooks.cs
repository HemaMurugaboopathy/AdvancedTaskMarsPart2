using AdvancedTaskSpecFlow.Utilities;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AdvancedTaskSpecFlow
{
    [Binding]
        public class Hooks : CommonDriver
        {
        public static ExtentReports extent;
        public static ExtentTest test;

        [BeforeTestRun]
        public static void ExtentStart()
        {
            extent = new ExtentReports();
            var SparkReporter = new ExtentSparkReporter("D:\\Hema\\IndustryConnect\\Internship\\AdvancedTaskPart2\\AdvancedTaskMarsPart2\\AdvancedTaskSpecFlow\\ExtentReports\\ExtentReport.html");
            extent.AttachReporter(SparkReporter);
        }
            [BeforeScenario]
            public void BeforeScenarioWithTag()
            {
                Initialize();
            test = extent.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            }

            [AfterScenario]
            public void AfterScenario()
            {
            // Capture screenshot if scenario fails
            if (ScenarioContext.Current.TestError != null)
            {
                string scenarioTitle = ScenarioContext.Current.ScenarioInfo.Title;
                Console.WriteLine($"Scenario '{scenarioTitle}' failed: {ScenarioContext.Current.TestError.Message}");
                CaptureScreenshot(scenarioTitle);
                test.Fail($"Scenario '{scenarioTitle}' failed: {ScenarioContext.Current.TestError.Message}");
            }
            else
            {
                test.Pass("Scenario passed");
            }
            Close();
            }
        [AfterTestRun]
        public static void ExtentClose()
        {
            extent.Flush();
        }

        public void CaptureScreenshot(string scenarioTitle)
        {
            string screenshotFileName = $"screenshot_{scenarioTitle}";
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string filePath = "D:\\Hema\\IndustryConnect\\Internship\\AdvancedTaskPart2\\AdvancedTaskMarsPart2\\AdvancedTaskSpecFlow\\ScreenShot";
            string screenshotPath = Path.Combine(filePath, $"{screenshotFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(screenshotPath);
        }
    }   
}
