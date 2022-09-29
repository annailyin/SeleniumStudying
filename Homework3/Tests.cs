using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using YandexMailClassLibrary;

namespace Homework3
{
    [TestFixture]
    public class Tests
    {
        private WebDriver _webDriver;
        private YandexMailBoxPage _mailBoxPage;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        public void LoginToYandexMailBox_UserEntersValidCredentials_LoginIsSuccessfullTest(string url, string username, string password)
        {

            LoginToYandexMailBox(url, username, password);

            Assert.Pass("Login test to Yandex has passed successfully!");
        }

        [Test]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        public void LogoutFromYandex_UserPressesLogout_LogoutIsSuccessfullTest(string url, string username, string password)
        {
            LoginToYandexMailBox(url, username, password);
            _mailBoxPage.Logout();

            Assert.Pass("Logout from Yandex has passed successfully!");
        }

        #region Private

        public void LoginToYandexMailBox(string url, string username, string password)
        {
            _webDriver.Navigate().GoToUrl(url);

            YandexStartPage startPage = new YandexStartPage(_webDriver);
            YandexMailHomePage mailHomePage = startPage.GoToMailHomePage();
            _mailBoxPage = mailHomePage.Login(username, password);
        }

        #endregion
    }
}