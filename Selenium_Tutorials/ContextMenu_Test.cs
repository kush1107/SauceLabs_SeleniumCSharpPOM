using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    public class ContextMenu_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/context_menu";

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
        public void ContextMenuTest()
        {

            //Using Explit wait to wait till element visible

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement text = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[normalize-space()='Context Menu']")));

            IWebElement contextClickOnElement = driver.FindElement(By.CssSelector("#hot-spot"));
            new Actions(driver)
                .ContextClick(contextClickOnElement)
                .Perform();

            IAlert alert =  driver.SwitchTo().Alert(); //switching to alert 
            Console.WriteLine(alert.Text); //print alert text 
            alert.Accept(); // click on ok button of alert 


        }
    }
}
