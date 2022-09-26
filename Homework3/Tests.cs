using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using YandexMailClassLibrary;

namespace Homework3
{
    public class Tests
    {
        private WebDriver _webDriver;
        private YandexMailBoxPage _mailBoxPage;

        [Test]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        public void LoginToYandexMailBox_UserEntersValidCredentials_LoginIsSuccessfullTest(string url, string username, string password)
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl(url);

            YandexStartPage startPage = new YandexStartPage(_webDriver);
            YandexMailHomePage mailHomePage = startPage.GoToMailHomePage();
            _mailBoxPage = mailHomePage.Login(username, password);

            Assert.Pass("Login test to Yandex has passed successfully!");
        }

        [Test]
        public void LogoutFromYandex_UserPressesLogout_LogoutIsSuccessfullTest()
        {
            if (_mailBoxPage != null)
            {
                _mailBoxPage.Logout();
                Assert.Pass("Logout from Yandex has passed successfully!");
                _webDriver?.Quit();
            }
            else
            {
                Assert.IsTrue(_mailBoxPage != null, "YandexMailBoxPage is empty! Should login before logout.");
            }
        }
    }
}