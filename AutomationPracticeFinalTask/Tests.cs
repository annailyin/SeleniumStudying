using AutomationPracticeClassLibrary.Entities;
using AutomationPracticeClassLibrary.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace AutomationPracticeFinalTask
{
    [TestFixtureSource(typeof(Tests.TestFixtureSource), nameof(Tests.TestFixtureSource.FixtureParams))]
    public partial class Tests
    {
        private const string _baseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        private WebDriver _webDriver;
        private User _user;
        private readonly string _browserName;

        public Tests(string browserName)
        {
            _browserName = browserName;
        }

        [SetUp]
        public void SetUp()
        {
            ReadJsonDataForAutomationPracticeAccount("Account.json");

            _webDriver = GetDriver(_browserName);
            _webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        [TestCase(Description = "Testing login creation for AutomationPractice site.")]
        public void CreateAutomationPracticeAccount_UserEntersValidData_AccountCreatedSuccessfullTest()
        {
            AutomationPracticeStartPage startPage = GoToAutomationPracticeStartPage();
            AutomationPracticeCreateAccountPage createAccountPage = startPage.GoToCreateAccountPage(_user);
            createAccountPage.CreateAccount(_user);
        }

        [Test]
        [TestCase(Description = "Testing login to AutomationPractice site.")]
        public void LoginWithinAutomationPracticeAccount_UserEntersLoginAndPassword_LoginIsSuccessfullTest()
        {
            LoginWithinAutomationPracticeAccount();
        }

        [Test]
        [TestCase(Description = "Testing automatical creation of a wishlist and adding products to it.")]
        public void UserIsAuthenticatedAndWishlistIsAutoCreatedIfNotExists_UserAddsProductsToAutoCreatedWishlist_NotificationAboutAddingProductsIsShown()
        {
            AutomationPracticeMyAccountPage myAccountPage = LoginWithinAutomationPracticeAccount();
            string confirmationText = myAccountPage.AddProductToAutoCreatedWishlist();

            Assert.AreEqual("Added to your wishlist.", confirmationText);
        }

        [Test]
        [TestCase(Description = "Testing manually creation of a wishlist and adding products to it.")]
        public void UserIsAuthenticatedAndWishlistIsCreatedManually_UserAddsProductsToManuallyCreatedWishlist_NotificationAboutAddingProductsIsShown()
        {
            AutomationPracticeMyAccountPage myAccountPage = LoginWithinAutomationPracticeAccount();
            string confirmationText = myAccountPage.AddProductToManuallyCreatedWishlist("test");

            Assert.AreEqual("Added to your wishlist.", confirmationText);
        }

        [Test(Description = "Testing adding products to the cart.")]
        [TestCase(3)]
        public void UserIsAuthenticated_UserAddsDifferentProductsToCart_AllAddedProductsAreInCart(int count)
        {
            AutomationPracticeMyAccountPage myAccountPage = LoginWithinAutomationPracticeAccount();

            Assert.IsTrue(myAccountPage.DifferentProductsAreAddedToCart(count));
        }

        #region Private

        public WebDriver GetDriver(string browserName)
        {
            return browserName switch
            {
                "Firefox" => new FirefoxDriver(),
                "Chrome" => new ChromeDriver(),
                "Edge" => new EdgeDriver(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

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