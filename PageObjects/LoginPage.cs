using OpenQA.Selenium;
using SauceLabsAutomationPOM.BaseTest;

namespace SauceLabsAutomationPOM.PageObjects
{
    public class LoginPage : PageLocatorActions
    {
        private readonly By usernameField = By.Id("user-name");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");
        
        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public void EnterUsername(string username) => enterTextByXpath(usernameField, username);
        public void EnterPassword(string password) => enterTextByXpath(passwordField, password);
        public void ClickLogin() => clickOnElementByXpath(loginButton);
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public string GetErrorMessage() => GetText(errorMessage);
        public bool IsLoginErrorDisplayed() => IsElementDisplayed(errorMessage);
    }
}