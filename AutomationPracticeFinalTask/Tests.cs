using AutomationPracticeClassLibrary.Entities;
using AutomationPracticeClassLibrary.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AutomationPracticeFinalTask
{
    [TestFixtureSource(typeof(Tests.TestFixtureSource), nameof(Tests.TestFixtureSource.FixtureParams))]
    public partial class Tests
    {
        private const string _baseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        private WebDriver _webDriver;
        private User _user;
        private readonly string _runType;
        private readonly string _browserName;
        private readonly string _browserVersion;
        private readonly string _platformName;


        public Tests(string runType, string browserName, string browserVersion, string platformName)
        {
            _runType = runType;
            _browserName = browserName;
            _browserVersion = browserVersion;
            _platformName = platformName;
        }

        [SetUp]
        public void SetUp()
        {
            ReadJsonDataForAutomationPracticeAccount("Account.json");

            _webDriver = GetDriver(_runType, _browserName, _browserVersion, _platformName);
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

        private WebDriver GetDriver(string runType, string browserName, string browserVersion, string platformName)
        {
            return runType switch
            {
                "Local" => browserName switch
                {
                    "Chrome" => new ChromeDriver(),
                    "Edge" => new EdgeDriver(),
                    _ => throw new ArgumentOutOfRangeException()
                },
                "Remote" => GetRemoteDriver(browserName, browserVersion, platformName),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private WebDriver GetRemoteDriver(string browserName, string browserVersion, string platformName)
        {
            var driverOptions = GetDriverOptions(browserName, browserVersion, platformName);
            var remoteAddress = new Uri("https://oauth-anna.ilyin-e951d:f7d5071a-fb1b-41fd-ba2e-fe43851ef6be@ondemand.eu-central-1.saucelabs.com:443/wd/hub");
            return new RemoteWebDriver(remoteAddress, driverOptions);
        }

        private DriverOptions GetDriverOptions(string browserName, string browserVersion, string platformName)
        {
            var options = GetBrowserSpecificDriverOptions(browserName);
            options.PlatformName = platformName;
            options.BrowserVersion = browserVersion;
            options.AddAdditionalOption("sauce:options", new Dictionary<string, object>
            {
                { "username", Environment.GetEnvironmentVariable("SAUCE_USERNAME") },
                { "accessKey", Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY") }
            });

            return options;
        }

        private DriverOptions GetBrowserSpecificDriverOptions(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--incognito");
                    return chromeOptions;
                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("--incognito");
                    return edgeOptions;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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