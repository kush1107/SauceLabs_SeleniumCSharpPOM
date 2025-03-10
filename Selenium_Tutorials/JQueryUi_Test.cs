using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class JQueryUi_Test
    {
        private IWebDriver driver;
        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        public static string downloadPath;

        private static string Url = "https://the-internet.herokuapp.com/jqueryui/menu";


        [OneTimeSetUp]
        public void SetUp()
        {
            if (driver == null)
            {
                downloadPath = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Downloads", "JQueryDownloads");

                // Ensure download directory exists
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }

                // Set Chrome options
                var options = new ChromeOptions();
                options.AddUserProfilePreference("download.default_directory", downloadPath);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);

                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize(); // Maximize the Browser Window
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20); // Adding implicit wait for page loading            

            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                Console.WriteLine("Closing the browser.");
                driver.Quit(); // Quit the browser
            }
        }

        [Test, Order(1)]
        public void JQueryUiTest1()
        {
            driver.Navigate().GoToUrl(Url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // wait till element visible
            IWebElement mneu1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[normalize-space()='Enabled']")));
            IWebElement mneu2 = driver.FindElement(By.XPath("//a[normalize-space()='Back to JQuery UI']"));

            Actions action = new Actions(driver);
            action
                .MoveToElement(mneu1)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu2)
                .Click()
                .Perform();

        }
        [Test, Order(2)]
        public void JQueryUiTest2_PDF()
        {
            driver.Navigate().GoToUrl(Url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait till element is clickable
            IWebElement mneu1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[normalize-space()='Enabled']")));
            IWebElement mneu2 = driver.FindElement(By.XPath("//a[normalize-space()='Downloads']"));
            IWebElement mneu3 = driver.FindElement(By.XPath("//a[normalize-space()='PDF']"));

            Actions action = new Actions(driver);
            action
                .MoveToElement(mneu1)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu2)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu3)
                .Click()
                .Perform();

            // Use FluentWait to wait for the file download completion
            string downloadedFile = WaitForFileDownloadUsingFluentWait(downloadPath, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));

            if (!string.IsNullOrEmpty(downloadedFile))
            {
                Console.WriteLine($"File downloaded successfully: {downloadedFile}");
            }
            else
            {
                Console.WriteLine("File download failed.");
            }
        }

        [Test, Order(3)]
        public void JQueryUiTest3_EXCEL()
        {
            driver.Navigate().GoToUrl(Url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait till element is clickable
            IWebElement mneu1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[normalize-space()='Enabled']")));
            IWebElement mneu2 = driver.FindElement(By.XPath("//a[normalize-space()='Downloads']"));
            IWebElement mneu3 = driver.FindElement(By.XPath("//a[normalize-space()='Excel']"));

            Actions action = new Actions(driver);
            action
                .MoveToElement(mneu1)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu2)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu3)
                .Click()
                .Perform();

            // Use FluentWait to wait for the file download completion
            string downloadedFile = WaitForFileDownloadUsingFluentWait(downloadPath, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));

            if (!string.IsNullOrEmpty(downloadedFile))
            {
                Console.WriteLine($"File downloaded successfully: {downloadedFile}");
            }
            else
            {
                Console.WriteLine("File download failed.");
            }
        }

        [Test, Order(4)]
        public void JQueryUiTest4_CSV()
        {
            driver.Navigate().GoToUrl(Url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait till element is clickable
            IWebElement mneu1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[normalize-space()='Enabled']")));
            IWebElement mneu2 = driver.FindElement(By.XPath("//a[normalize-space()='Downloads']"));
            IWebElement mneu3 = driver.FindElement(By.XPath("//a[normalize-space()='CSV']"));

            Actions action = new Actions(driver);
            action
                .MoveToElement(mneu1)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu2)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(mneu3)
                .Click()
                .Perform();

            // Use FluentWait to wait for the file download completion
            string downloadedFile = WaitForFileDownloadUsingFluentWait(downloadPath, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));

            if (!string.IsNullOrEmpty(downloadedFile))
            {
                Console.WriteLine($"File downloaded successfully: {downloadedFile}");
            }
            else
            {
                Console.WriteLine("File download failed.");
            }
        }


        // Uses FluentWait to wait for a new file to appear in the specified directory.
        // <param name="directory">The download directory path.</param>
        // <param name="timeout">Maximum wait time for the file to appear.</param>
        // <param name="pollingInterval">Polling interval to check for the file.</param>
        // <returns>The name of the latest file if found, otherwise null.</returns>
        private string WaitForFileDownloadUsingFluentWait(string directory, TimeSpan timeout, TimeSpan pollingInterval)
        {
            DefaultWait<string> fluentWait = new DefaultWait<string>(directory)
            {
                Timeout = timeout,
                PollingInterval = pollingInterval
            };

            fluentWait.IgnoreExceptionTypes(typeof(IOException));

            return fluentWait.Until(dir =>
            {
                var latestFile = new DirectoryInfo(dir).GetFiles()
                                    .OrderByDescending(f => f.LastWriteTime)
                                    .FirstOrDefault();

                if (latestFile == null || latestFile.Extension == ".crdownload" || latestFile.Extension == ".part" || latestFile.Extension == ".tmp")
                {
                    return null; // Wait until the file is fully downloaded
                }

                // Ensure the file size remains constant before considering the download complete
                long initialSize = latestFile.Length;
                Thread.Sleep(1000); // Wait 1 second
                long newSize = latestFile.Length;

                return initialSize == newSize ? latestFile.Name : null;
            });


        }

    }
}
