using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class DynamicLoading_Test
    {
        private  IWebDriver driver;

       

        private static string Url = "https://the-internet.herokuapp.com/dynamic_loading";

        [OneTimeSetUp]
        public void SetUp()
        {


            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize(); // Maximize the Browser Window
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //Adding implicit wait for page loading            
            }
        }


        [OneTimeTearDown]
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
        public void DynamicControlsTest1()
        {
            driver.Navigate().GoToUrl(Url);  //Navigate to URL
            Console.WriteLine($"Test execution started for URL: {Url}");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement link1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space()='Example 1: Element on page that is hidden']")));
            link1.Click();

            // wait till element visible
            IWebElement startBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[normalize-space()='Start']")));
            startBtn.Click();

            IWebElement loader1 = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("img[src='/img/ajax-loader.gif']")));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("img[src='/img/ajax-loader.gif']")));

            IWebElement text1 = driver.FindElement(By.XPath("//h4[normalize-space()='Hello World!']"));
            if (text1.Displayed)
            {
                Console.WriteLine("It's Hello World displayed text - " + text1.Text);
            }


        }

        [Test]
        public void DynamicControlsTest2()
        {
            driver.Navigate().GoToUrl(Url);  //Navigate to URL
            Console.WriteLine($"Test execution started for URL: {Url}");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement link1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space()='Example 2: Element rendered after the fact']")));
            link1.Click();

            // wait till element visible
            IWebElement startBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[normalize-space()='Start']")));
            startBtn.Click();

            IWebElement loader1 = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("img[src='/img/ajax-loader.gif']")));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("img[src='/img/ajax-loader.gif']")));

            IWebElement text1 = driver.FindElement(By.XPath("//h4[normalize-space()='Hello World!']"));
            if (text1.Displayed)
            {
                Console.WriteLine("It's Hello World displayed text - " + text1.Text);
            }


        }
    }
}
