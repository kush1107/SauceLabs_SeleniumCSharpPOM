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
    public class DisappearingElements_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/disappearing_elements";

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

        public void DisappearingElementsTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            while (true) // Keep refreshing until the element appears
            {
                try
                {
                    // Locate elements after each refresh
                    IWebElement homePageTab = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[normalize-space()='Home']")));
                    IWebElement galleryTab = driver.FindElement(By.XPath("//a[normalize-space()='Gallery']"));

                    if (galleryTab.Displayed)  // If it's found, print message and break loop
                    {
                        Console.WriteLine("Gallery Tab is displayed .....");
                        break;
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Gallery Tab not found, refreshing the page...");
                }

                driver.Navigate().Refresh(); // Refresh the page and try again
            }


        }
    }
}
