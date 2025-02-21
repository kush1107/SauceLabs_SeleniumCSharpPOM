using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;
using NUnit.Framework.Legacy;

namespace SauceLabsAutomationPOM.Selenium_Tutorials
{
    [TestFixture]
    public class HandleAlertsPopup_Test
    {
        private IWebDriver driver;
        private static readonly string Url = "https://the-internet.herokuapp.com/javascript_alerts";

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
                driver.Quit(); // Ensures complete WebDriver session termination
            }
        }

        [Test, Order(1)]
        public void HandleAlertsPopupTest1()
        {
            var expectedAlertText = "I am a JS Alert";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement clickForJsAlert = driver.FindElement(By.CssSelector("button[onclick='jsAlert()']"));
            clickForJsAlert.Click();

            // Wait for alert to be present
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Console.WriteLine(alert.Text);
            ClassicAssert.AreEqual(expectedAlertText, alert.Text);

            alert.Accept(); // Accept the alert

            Console.WriteLine("Alert handled successfully.");
        }

        [Test, Order(2)]
        public void HandleAlertsPopupTest2()
        {
            var expectedAlertText = "I am a JS Confirm";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement clickForJsConfirm = driver.FindElement(By.CssSelector("button[onclick='jsConfirm()']"));
            clickForJsConfirm.Click();

            // Wait for alert to be present
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Console.WriteLine(alert.Text);
            ClassicAssert.AreEqual(expectedAlertText, alert.Text);

            alert.Accept(); // Accept the alert

            Console.WriteLine("Alert handled successfully.");
        }

        [Test, Order(3)]
        public void HandleAlertsPopupTest3()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement clickForJsPromt = driver.FindElement(By.CssSelector("button[onclick='jsPrompt()']"));
            clickForJsPromt.Click();

            // Wait for alert to be present
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            alert.SendKeys("I'm Automation Tester!!");
            alert.Accept(); // Accept the alert

            Console.WriteLine("Alert handled successfully.");
        }
    }
}
