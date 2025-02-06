using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    public class BasicAuth_Test
    {
        private IWebDriver driver;


        private static string Url = "the-internet.herokuapp.com/basic_auth";
        private static string username= "admin",password="admin";
        [SetUp]
        public void SetUp()
        {


            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize(); // Maximize the Browser Window
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //Adding implicit wait for page loading
                                                                                    // ... Get the URL
                driver.Navigate().GoToUrl("https://"+username+":"+password+"@"+Url);
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
        public void BasicAuthTest() {
            Console.WriteLine("Basic Auth - Passed.");

        }
    }
}
