using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class FileDownload
    {
        private IWebDriver driver;
        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        public static string downloadPath;

        private static string Url = "https://the-internet.herokuapp.com/download";

        [OneTimeSetUp]
        public void SetUp()
        {
            if (driver == null)
            {
                downloadPath = Path.Combine(projectDirectory, "AutomationFiles", "Results", "Downloads");

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

        [Test]
        public void FileDownloadTest1()
        {
            driver.Navigate().GoToUrl(Url);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            // Click on a file to download (Selecting the first available file)
            IWebElement fileLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space()='20MB.bin']")));
            string fileName = fileLink.Text; // Get the file name dynamically
            Console.WriteLine($"Downloading File: {fileName}");
            fileLink.Click();

            // Wait dynamically until file appears in the download folder
            string downloadedFilePath = Path.Combine(downloadPath, fileName);
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => File.Exists(downloadedFilePath));

            // Get the latest downloaded file and print its name
            var latestFile = new DirectoryInfo(downloadPath).GetFiles()
                                .OrderByDescending(f => f.LastWriteTime)
                                .FirstOrDefault();

            if (latestFile != null)
            {
                Console.WriteLine($"File downloaded successfully: {latestFile.Name}");
            }
            else
            {
                Console.WriteLine("File download failed.");
            }
        }

        [Test]
        public void FileDownloadTest2()
        {
            driver.Navigate().GoToUrl(Url);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            // Click on a file to download (Selecting the first available file)
            IWebElement fileLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space()='samplePDF.pdf']")));
            string fileName = fileLink.Text; // Get the file name dynamically
            Console.WriteLine($"Downloading File: {fileName}");
            fileLink.Click();

            // Wait dynamically until file appears in the download folder
            string downloadedFilePath = Path.Combine(downloadPath, fileName);
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => File.Exists(downloadedFilePath));

            // Get the latest downloaded file and print its name
            var latestFile = new DirectoryInfo(downloadPath).GetFiles()
                                .OrderByDescending(f => f.LastWriteTime)
                                .FirstOrDefault();

            if (latestFile != null)
            {
                Console.WriteLine($"File downloaded successfully: {latestFile.Name}");
            }
            else
            {
                Console.WriteLine("File download failed.");
            }
        }
    }
}
