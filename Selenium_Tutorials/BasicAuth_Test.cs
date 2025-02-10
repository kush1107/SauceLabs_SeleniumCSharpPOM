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

    //Basically Basic Auth & Disgest Auth handling is by same implementation


    [TestFixture]
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
                                                                                 
                driver.Navigate().GoToUrl("https://"+username+":"+password+"@"+Url);  //Navigate to URL like this for Basic Auth By pass

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

            Thread.Sleep(3000); // Static sleep to see page actions  - if you know explicit wait then use it instead which is given in below commented code

            IWebElement text = driver.FindElement(By.XPath("//p[contains(text(),'Congratulations! You must have the proper credenti')]"));

            /*WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement text = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Congratulations! You must have the proper credenti')]")));*/
           
            if (text.Displayed)
            {
                Console.WriteLine("Basic Auth - Passed.");
            }
            

        }
    }
}
