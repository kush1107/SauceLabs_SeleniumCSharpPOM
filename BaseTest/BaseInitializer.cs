using System.Reflection;
using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;


namespace SauceLabsAutomationPOM.BaseTest
{
    public class BaseInitializer
    {
        public static  IWebDriver? driver; // Make nullable to prevent CS8618 warning
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseInitializer));

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            // Ensure log4net is loading the config file
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // Check if the 'Logs' directory exists, and create it if it doesn't
            string logDirectory = "Logs";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory); // Creates the directory if it doesn't exist
                Console.WriteLine("Logs directory created.");
            }

            logger.Info("Log4net configuration loaded.");
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
                driver.Dispose(); // Fix for NUnit1032: Ensure disposal
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
                driver.Dispose(); // Ensure disposal
                driver = null;
                logger.Info("Test execution completed.");
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
    }
}
