using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using YandexMailClassLibrary;

namespace Homework3
{
    [TestFixtureSource(typeof(Tests.TestFixtureSource), nameof(Tests.TestFixtureSource.FixtureParams))]
    public partial class Tests
    {
        private WebDriver _webDriver;
        private readonly string _browserName;
        private readonly string _browserVersion;
        private readonly string _platformName;

        public Tests(string browserName, string browserVersion, string platformName)
        {
            _browserName = browserName;
            _browserVersion = browserVersion;
            _platformName = platformName;
        }

        [SetUp]
        public void SetUp()
        {
            _webDriver = GetDriver(_browserName, _browserVersion, _platformName);
            _webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test(Description = "Testing login to yandex.com")]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        public void LoginToYandexMailBox_UserEntersValidCredentials_LoginIsSuccessfullTest(string url, string username, string password)
        {

            LoginToYandexMailBox(url, username, password);

            Assert.Pass("Login test to Yandex has passed successfully!");
        }

        [Test(Description = "Testing logout from yandex.com")]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        public void LogoutFromYandex_UserPressesLogout_LogoutIsSuccessfullTest(string url, string username, string password)
        {
            YandexMailBoxPage mailBoxPage = LoginToYandexMailBox(url, username, password);
            mailBoxPage.Logout();

            Assert.Pass("Logout from Yandex has passed successfully!");
        }

        #region Private

        public YandexMailBoxPage LoginToYandexMailBox(string url, string username, string password)
        {
            _webDriver.Navigate().GoToUrl(url);
            TakeAndSaveScreenShotInCurrentDirectory();

            YandexStartPage startPage = new YandexStartPage(_webDriver);
            YandexMailHomePage mailHomePage = startPage.GoToMailHomePage();

            return mailHomePage.Login(username, password);
        }

        public void TakeAndSaveScreenShotInCurrentDirectory()
        {
            try
            {
                Screenshot image = ((ITakesScreenshot)_webDriver).GetScreenshot();
                String path = String.Concat(new DirectoryInfo(".").FullName, "\\Screenshot.png");
                image.SaveAsFile(path, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception("Screenshot hasn't been saved!", e);
            }
        }

        public WebDriver GetDriver(string browserName, string browserVersion, string platformName)
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
                case "Firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("--incognito");
                    return firefoxOptions;
                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("--incognito");
                    return edgeOptions;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}