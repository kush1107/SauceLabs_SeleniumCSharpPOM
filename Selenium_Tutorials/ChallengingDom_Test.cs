using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class ChallengingDom_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/challenging_dom";

        [SetUp]
        public void SetUp()
        {


            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize(); // Maximize the Browser Window
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //Adding implicit wait for page loading
                driver.Navigate().GoToUrl(Url);  //Navigate to URL
                Console.WriteLine($"Test execution started for URL: {Url}");

            }
        }


        [TearDown]
        public void TearDown()
        {
            // Clean up the WebDriver instance after each test
            if (driver != null)
            {
                Console.WriteLine("Closing the browser after each test.");
                driver.Close(); //Closing the browser window
            }
        }

        [Test]
        public void ChallengingDomTest()
        {
            // Extract JavaScript block that contains the answer
            IWebElement scriptElement = driver.FindElement(By.XPath("//div[@id='content']/script"));
            string scriptBlock = scriptElement.GetAttribute("innerHTML");

            // Use Regex to extract the number after 'Answer:'
            Match match = Regex.Match(scriptBlock, @"Answer:\s(\d+)");
            if (match.Success)
            {
                string oldAnswer = match.Groups[1].Value;
                Console.WriteLine("Found current Answer value: " + oldAnswer);
            }

            // Refresh the page
            driver.Navigate().Refresh();

            // Extract script block again after refresh
            scriptElement = driver.FindElement(By.XPath("//div[@id='content']/script"));
            scriptBlock = scriptElement.GetAttribute("innerHTML");

            // Extract new answer value
            match = Regex.Match(scriptBlock, @"Answer:\s(\d+)");
            if (match.Success)
            {
                string newAnswer = match.Groups[1].Value;
                Console.WriteLine("Found new Answer value: " + newAnswer);
            }
        }
    }
}
