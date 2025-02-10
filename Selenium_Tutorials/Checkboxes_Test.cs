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
    public class Checkboxes_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/checkboxes";

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
        public void CheckboxesTest() {

            //Using Explit wait to wait till element visible

            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));
            IWebElement chk1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//input[@type='checkbox'])[1]")));

            IWebElement chk2 = driver.FindElement(By.XPath("(//input[@type='checkbox'])[2]"));
            if (!chk1.Selected)
            {
                chk1.Click();
                Console.WriteLine("CLicked on Checkbox1...");
            }
            else {
                Console.WriteLine("ALready checked  Checkbox1...");
            }

            if (!chk2.Selected)
            {
                chk2.Click();
                Console.WriteLine("CLicked on Checkbox2...");
            }
            else
            {
                Console.WriteLine("ALready checked  Checkbox2...");
            }


        }
    }
}
