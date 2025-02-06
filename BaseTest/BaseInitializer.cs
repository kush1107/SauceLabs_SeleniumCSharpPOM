using Newtonsoft.Json;
using System.IO;
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

        // Read properties
        public static string Url { get; private set; }
        public static string Browser { get; private set; }
        public static string Username { get; private set; }
        public static string Password { get; private set; }

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            LoadProperties();
            SetLogConfigurations();  // This will use only log4net for logging
            SetExtentReports();
        }

        [SetUp]
        public void SetUp()
        {
            // Create a new test in ExtentReports
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            if (driver == null)
            {
                driver = InitializeBrowser(Browser);  // Use the browser read from the properties file
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl(Url);  // Use the URL read from the properties file
                LogInfo($"Test execution started. Browser: {Browser}, URL: {Url}");

            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                LogFail("Test Failed");
                test.Fail("Test Failed");
            }
            else
            {
                LogPass("Test Passed");
                test.Pass("Test Passed");
            }

            // Clean up the WebDriver instance after each test
            if (driver != null)
            {
                LogInfo("Closing the browser after each test.");
                driver.Quit();
                driver.Dispose();
                driver = null;
                LogInfo("Test execution completed for this test.");
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            // Global teardown for closing any remaining instances of WebDriver
            if (driver != null)
            {
                LogInfo("Closing the browser after all tests.");
                driver.Quit();
                driver.Dispose();
                driver = null;
                LogInfo("All test executions completed.");
            }
            else
            {
                LogWarn("Driver was already null during teardown.");
            }

            // Flush ExtentReports to generate the final report
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
                LogFail($"Error initializing browser: {ex.Message}");
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

            LogInfo("Global setup initialized.");
        }

        public void SetExtentReports()
        {
            string reportDirectory = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Reports");
            Directory.CreateDirectory(reportDirectory);

            string reportFilePath = Path.Combine(reportDirectory, $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

            var htmlReporter = new ExtentSparkReporter(reportFilePath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            LogInfo("Extent Reports setup completed.");
        }

        // Custom method to load properties from JSON file
        public static void LoadProperties()
        {
            // Set the path to the JSON file
            string jsonFilePath = Path.Combine(projectDirectory, "AutomationFiles", "Resources", "properties", "env_QA.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"JSON configuration file not found: {jsonFilePath}");
            }

            // Read the JSON file and deserialize it into an object
            var jsonData = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

            if (config == null)
            {
                throw new InvalidDataException("Failed to parse JSON configuration.");
            }

            // Assign values from the JSON configuration
            Url = config.GetValueOrDefault("url", "https://www.saucedemo.com/");
            Browser = config.GetValueOrDefault("browser", "chrome");
            Username = config.GetValueOrDefault("username", "standard_user");
            Password = config.GetValueOrDefault("password", "secret_sauce");

            // Optionally log the properties (sensitive data like passwords should not be logged in production)
            LogInfo($"Properties loaded: URL: {Url}, Browser: {Browser}");
        }

        // Custom method to log messages to both log4net and ExtentReports
        private static void LogInfo(string message)
        {
            logger.Info(message);
            test?.Info(message);
        }

        private static void LogWarn(string message)
        {
            logger.Warn(message);
            test?.Warning(message);
        }

        private static void LogFail(string message)
        {
            logger.Fatal(message);
            test?.Fail(message);
        }

        private static void LogPass(string message)
        {
            logger.Info(message);
            test?.Pass(message);
        }

       
    }
}
