using System.Reflection;
using System.Xml.Linq;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SauceLabsAutomationPOM.BaseTest
{
    public class BaseInitializer
    {
        protected static IWebDriver? driver;
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseInitializer));
        private static ExtentReports extent;
        private static ExtentTest test;
        
        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName; 

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            SetLogConfigurations();
            SetExtentReports();
        }

        [SetUp]
        public void SetUp()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            
            if (driver == null)
            {
                string browser = TestContext.Parameters.Get("browser", "chrome");
                driver = InitializeBrowser(browser);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl("https://www.saucedemo.com/");
                logger.Info($"Test execution started. Browser: {browser}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed");
            }
            else
            {
                test.Pass("Test Passed");
            }

            if (driver != null)
            {
                logger.Info("Closing the browser after each test.");
                driver.Quit();
                driver.Dispose();
                driver = null;
                logger.Info("Test execution completed for this test.");
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            if (driver != null)
            {
                logger.Info("Closing the browser after all tests.");
                driver.Quit();
                driver.Dispose();
                driver = null;
                logger.Info("All test executions completed.");
            }
            else
            {
                logger.Warn("Driver was already null during teardown.");
            }

            // Flush ExtentReports
            extent.Flush();
        }

        private IWebDriver InitializeBrowser(string browser)
        {
            try
            {
                return browser.ToLower() switch
                {
                    "chrome" => new ChromeDriver(),
                    "firefox" => new FirefoxDriver(),
                    "edge" => new EdgeDriver(),
                    _ => throw new ArgumentException($"Unsupported browser: {browser}")
                };
            }
            catch (Exception ex)
            {
                logger.Error($"Error initializing browser: {ex.Message}");
                throw;
            }
        }

        public void SetLogConfigurations()
        {
            string logDirectory = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Logs");
            Directory.CreateDirectory(logDirectory);

            string logFilePath = Path.Combine(logDirectory, $"SauceLabs_Logs_{DateTime.Now:yyyyMMdd_HHmmss}.log");

            Environment.SetEnvironmentVariable("APP_LOG_PATH", logFilePath);
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            logger.Info("Global setup initialized.");
        }

        public void SetExtentReports()
        {
            string reportDirectory = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Reports");
            Directory.CreateDirectory(reportDirectory);

            string reportFilePath = Path.Combine(reportDirectory, $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

            var htmlReporter = new ExtentSparkReporter(reportFilePath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            logger.Info("Extent Reports setup completed.");
        }
    }
}
