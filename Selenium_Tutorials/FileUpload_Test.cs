using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class FileUpload_Test
    {
        private IWebDriver driver;
        private static readonly string Url = "https://the-internet.herokuapp.com/upload";

        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        public static string uploadPath;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(Url);
            Console.WriteLine($"Test execution started for URL: {Url}");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                Console.WriteLine("Closing the browser after each test.");
                driver.Close(); // Ensures complete WebDriver session termination
            }
        }

        [Test]
        public void FileUploadTest() {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement uploadBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#file-submit")));
            if (uploadBtn.Displayed) {

                uploadPath = Path.Combine(projectDirectory, "AutomationFiles", "Resources", "FileUploads");

                // Ensure download directory exists
                if (Directory.Exists(uploadPath))
                {
                    IWebElement fileUpload = driver.FindElement(By.CssSelector("#file-upload"));

                    fileUpload.SendKeys(uploadPath + "\\FileUploadDemo.png");

                    uploadBtn.Click();

                    IWebElement fileUploadedTxt = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//h3[normalize-space()='File Uploaded!']")));

                    if (fileUploadedTxt.Displayed)
                    {
                        Console.WriteLine(fileUploadedTxt.Text);

                        Console.WriteLine("File Uploaded successfully.... ");
                    }

                }
                else {
                    Console.WriteLine("Please check the upload folder or file exist or not!! ");
                }

               
            }
        }
    }
}
