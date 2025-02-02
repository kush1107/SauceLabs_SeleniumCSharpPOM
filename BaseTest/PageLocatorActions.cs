using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SauceLabsAutomationPOM.BaseTest
{
    public class PageLocatorActions :BaseInitializer
    {
        protected IWebDriver driver;
        
        public PageLocatorActions(IWebDriver driver)
        {
            this.driver = driver;
        }


        public void clickOnElementByXpath(By locator)
        {
            IWebElement ele = driver.FindElement(locator);
            ele.Click();
        }
        
        public void enterTextByXpath(By locator,String textValue)
        {
            IWebElement ele = driver.FindElement(locator);
            ele.SendKeys(textValue);
        }
        

        public string GetText(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Text;
        }

        public bool IsElementDisplayed(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); 
                return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}