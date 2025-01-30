using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SauceLabsAutomationPOM.BaseTest
{
    public class PageLocatorActions
    {
        protected IWebDriver driver;
        private readonly WebDriverWait wait;

        public PageLocatorActions(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void Click(By locator)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }

        public void SendKeys(By locator, string text)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            element.Clear();
            element.SendKeys(text);
        }

        public string GetText(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Text;
        }

        public bool IsElementDisplayed(By locator)
        {
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}