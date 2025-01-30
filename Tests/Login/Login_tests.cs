using NUnit.Framework;
using SauceLabsAutomationPOM.BaseTest;
using SauceLabsAutomationPOM.PageObjects;

namespace SauceLabsAutomationPOM.Tests.Login
{
    [TestFixture]
    public class LoginTests : BaseInitializer
    {
        private LoginPage loginPage;

        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void Test_Login_With_Valid_Credentials()
        {
            loginPage.Login("standard_user", "secret_sauce");
            Assert.That(driver.Url, Does.Contain("inventory.html"), "Login failed with valid credentials.");
        }

        [Test]
        public void Test_Login_With_Invalid_Credentials()
        {
            loginPage.Login("invalid_user", "wrong_password");
            Assert.That(loginPage.IsLoginErrorDisplayed(), Is.True, "Error message not displayed for invalid login.");
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("Epic sadface"), "Incorrect error message.");
        }
    }
}