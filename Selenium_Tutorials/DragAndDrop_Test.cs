using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class DragAndDrop_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/drag_and_drop";

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
        public void DragAndDropTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // wait till element visible
            IWebElement boxA = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='column-a']")));

            IWebElement boxB =driver.FindElement(By.XPath("//div[@id='column-b']"));

            Actions action = new Actions(driver);
            action.DragAndDrop(boxA, boxB).Perform();

            Console.WriteLine("Box A is drag & dropped to Box B..");
        }
    }
}
