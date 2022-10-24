using AutomationPracticeClassLibrary.Entities;
using AutomationPracticeClassLibrary.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Text.Json;

namespace AutomationPracticeFinalTask
{
    [TestFixture]
    public class Tests
    {
        private const string _baseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        private WebDriver _webDriver;

        private User _user;

        [SetUp]
        public void Setup()
        {
            ReadJsonDataForAutomationPracticeAccount("Account.json");

            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        public void CreateAutomationPracticeAccount_UserEntersValidData_AccountCreatedSuccessfullTest()
        {
            AutomationPracticeStartPage startPage = GoToAutomationPracticeStartPage();
            AutomationPracticeCreateAccountPage createAccountPage = startPage.GoToCreateAccountPage(_user);
            createAccountPage.CreateAccount(_user);
        }

        [Test]
        public void LoginWithinAutomationPracticeAccount_UserEntersLoginAndPassword_LoginIsSuccessfullTest()
        {
            LoginWithinAutomationPracticeAccount();
        }

        [Test]
        public void AddToAutoCreatedWishList_UserAddsProductsToWishList_ProductsAddedToAutoCreatedWishListSuccessfullTest()
        {
            AutomationPracticeMyAccountPage myAccountPage = LoginWithinAutomationPracticeAccount();
            myAccountPage.GoToMyWishlists();
        }

        #region Private

        private void ReadJsonDataForAutomationPracticeAccount(string fileName)
        {
            _user = new User();

            string jsonString = File.ReadAllText(fileName);
            _user = JsonSerializer.Deserialize<User>(jsonString);
        }

        private AutomationPracticeStartPage GoToAutomationPracticeStartPage()
        {
            _webDriver.Navigate().GoToUrl(_baseURL);

            return new AutomationPracticeStartPage(_webDriver);
        }

        private AutomationPracticeMyAccountPage LoginWithinAutomationPracticeAccount()
        {
            AutomationPracticeStartPage startPage = GoToAutomationPracticeStartPage();

            return startPage.Login(_user);
        }

        #endregion
    }
}