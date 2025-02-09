using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class Dropdown_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/dropdown";

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
        public void DropdownTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // wait till element visible
            IWebElement dropDown = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@id='dropdown']")));

            SelectElement select = new SelectElement(dropDown);

            select.SelectByIndex(1);

            Thread.Sleep(2000); // Using sleep to see the select action on page - avoid using it in real project

            select.SelectByText("Option 2");

            Thread.Sleep(2000);

            select.SelectByValue("1");

            Thread.Sleep(2000);


        }
    }
}
