using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using OpenQA.Selenium.Chrome;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
     class AddRemoveElements_Test
    {
        private  IWebDriver driver;


        private static string Url = "https://the-internet.herokuapp.com/add_remove_elements/";
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
        public void AddRemoveElementsTest()
        {
            //Adding A Element
            IWebElement addElementBtn = driver.FindElement(By.XPath("//button[normalize-space()='Add Element']"));
            addElementBtn.Click();

            //Static wait to see the action  - avoid in real time project - incase use explicit or fluent wait
            Thread.Sleep(2000); //wait for 2 sec

            //Delete A Element
            IWebElement deleteElementBtn = driver.FindElement(By.XPath("//button[normalize-space()='Delete']"));
            deleteElementBtn.Click();

            Thread.Sleep(2000);

        }
    }
}
