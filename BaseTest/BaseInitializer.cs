using System.Reflection;
using System.Xml.Linq;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SauceLabsAutomationPOM.BaseTest
{
    public class BaseInitializer
    {
        protected static IWebDriver? driver;
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseInitializer));
        
        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName; // C:\Users\kusha\SauceLabsDemo_POM\  - path to project directory

        [OneTimeSetUp]
        public void GlobalSetUp()

        {
            setLogConfigurations();

        }

        [SetUp]
        public void SetUp()
        {
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

        public void setLogConfigurations()
        {
            // Define log folder path
            string logDirectory = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Logs");

            // Ensure the log directory exists
            Directory.CreateDirectory(logDirectory);

            // Generate log file name with timestamp
            string logFilePath = Path.Combine(logDirectory, $"SauceLabs_Logs_{DateTime.Now:yyyyMMdd_HHmmss}.log");

            // Set log file path dynamically
            Environment.SetEnvironmentVariable("APP_LOG_PATH", logFilePath);
            // Configure log4net
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            logger.Info("Global setup initialized.");
        }

    }
}
