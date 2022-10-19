using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using YandexMailClassLibrary;

namespace Homework3
{
    [TestFixture]
    public class Tests
    {
        private WebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions
            {  
                PlatformName = "Windows 10",
                BrowserVersion = "latest",
            };

            options.AddArguments("--incognito");

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("username", Environment.GetEnvironmentVariable("SAUCE_USERNAME"));
            sauceOptions.Add("accessKey", Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY"));
            options.AddAdditionalOption("sauce:options", sauceOptions);

            _webDriver = new RemoteWebDriver(new Uri("https://oauth-anna.ilyin-e951d:f7d5071a-fb1b-41fd-ba2e-fe43851ef6be@ondemand.eu-central-1.saucelabs.com:443/wd/hub"), options);
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

        #endregion
    }
}