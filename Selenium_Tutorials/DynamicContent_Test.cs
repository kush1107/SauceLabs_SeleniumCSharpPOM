using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class DynamicContent_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/dynamic_content?with_content=static";

        By dynamicTextLocator = By.XPath("(//div[@class='large-10 columns'])[3]");

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(Url);
            Console.WriteLine($"Test execution started for URL: {Url}");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                Console.WriteLine("Closing the browser after each test.");
                driver.Close(); // Use Quit instead of Close to properly end WebDriver session
            }
        }

        [Test]
        public void DynamicContentTest()
        {
            // Fetch text before clicking
            IWebElement dynamicTextElement = driver.FindElement(dynamicTextLocator);
            Console.WriteLine("Before Clicking on 'click here' link - " + dynamicTextElement.Text);

            // Click the "click here" link
            IWebElement clickHereLink = driver.FindElement(By.XPath("//a[normalize-space()='click here']"));
            clickHereLink.Click();

            // Re-fetch the dynamic text element after the page updates
            dynamicTextElement = driver.FindElement(dynamicTextLocator);
            Console.WriteLine("After Clicking on 'click here' link - " + dynamicTextElement.Text);
        }
    }
}
