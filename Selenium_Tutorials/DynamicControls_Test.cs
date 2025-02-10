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
    public class DynamicControls_Test
    {

        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/dynamic_controls";

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
        public void DynamicControlsTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement removeBtn = driver.FindElement(By.XPath("//button[normalize-space()='Remove']"));
            removeBtn.Click();

            // wait till element visible
            IWebElement loader = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@src='/img/ajax-loader.gif']")));
            

            IWebElement text = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'gone')]")));
            if (text.Displayed)
            {
                Console.WriteLine("It's displayed text - "+text.Text);   
            }

            IWebElement enableBtn = driver.FindElement(By.XPath("//button[normalize-space()='Enable']"));
            enableBtn.Click();

            // wait till element visible

            IWebElement enableText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'enabled')]")));

            IWebElement inputField = driver.FindElement(By.CssSelector("input[type='text']"));
            if (inputField.Enabled)
            {
                inputField.SendKeys("Hello QA Tester!!");
            }




        }
    }
}
