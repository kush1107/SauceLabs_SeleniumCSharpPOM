using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace SauceLabsAutomationPOM.SpecFlowDemoTest.StepDefinitionsFIle
{
    [Binding]
    public class LoginStepDefinitions
    {
        private IWebDriver driver;

        [BeforeScenario]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //Adding implicit wait for page loading
        }

        [Given(@"I navigate to the login page")]
        public void GivenIOpenTheLoginPage()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        }

        [When(@"I enter the username ""(.*)""")]
        public void WhenIEnterTheUsername(string username)
        {
            var usernameField = driver.FindElement(By.XPath("//input[@id='username']"));
            usernameField.Clear();
            usernameField.SendKeys(username);
        }

        [When(@"I enter the password ""(.*)""")]
        public void WhenIEnterThePassword(string password)
        {
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            var loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            ClassicAssert.IsTrue(driver.Url.Contains("/secure"), "User is not redirected to the dashboard.");
        }

        [Then(@"I should see the welcome message ""(.*)""")]
        public void ThenIShouldSeeTheWelcomeMessage(string expectedMessage)
        {
            var welcomeMessage = driver.FindElement(By.XPath("//h4[contains(text(),'Welcome to the Secure Area')]")).Text;
            ClassicAssert.AreEqual(expectedMessage, welcomeMessage, "Welcome message does not match.");
        }

        [AfterScenario]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
