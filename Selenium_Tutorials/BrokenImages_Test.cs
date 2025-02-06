using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    public class BrokenImages_Test
    {
        private IWebDriver driver;
        private static string Url = "https://the-internet.herokuapp.com/broken_images";

        [SetUp]
        public void SetUp()
        {


            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize(); // Maximize the Browser Window
                driver.Manage().Timeouts().ImplicitWait =TimeSpan.FromSeconds(10); //Adding implicit wait for page loading
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

        public void BrokenImagesTest()
        {
            // Find all images
            IList<IWebElement> images = driver.FindElements(By.TagName("img"));
            Console.WriteLine($"Total images found: {images.Count}");

            foreach (var img in images)
            {
                bool isBroken = (bool)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return arguments[0].naturalWidth == 0;", img);

                if (isBroken)
                {
                    Console.WriteLine($"Broken Image Found: {img.GetAttribute("src")}");
                }
            }
        }
    }
}
