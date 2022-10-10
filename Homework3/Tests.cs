using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567", "C:\\Temp\\Screenshots")]
        public void LoginToYandexMailBox_UserEntersValidCredentials_LoginIsSuccessfullTest(string url, string username, string password, string path)
        {

            LoginToYandexMailBox(url, username, password, path);

            Assert.Pass("Login test to Yandex has passed successfully!");
        }

        [Test]
        [TestCase("https://yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567", "C:\\Temp\\Screenshots")]
        public void LogoutFromYandex_UserPressesLogout_LogoutIsSuccessfullTest(string url, string username, string password, string path)
        {
            YandexMailBoxPage mailBoxPage = LoginToYandexMailBox(url, username, password, path);
            mailBoxPage.Logout();

            Assert.Pass("Logout from Yandex has passed successfully!");
        }

        #region Private

        public YandexMailBoxPage LoginToYandexMailBox(string url, string username, string password, string path)
        {
            _webDriver.Navigate().GoToUrl(url);

            CreateDirectoryIfMissing(path);
            TakeAndSaveScreenShotByPath(path);

            YandexStartPage startPage = new YandexStartPage(_webDriver);
            YandexMailHomePage mailHomePage = startPage.GoToMailHomePage();

            return mailHomePage.Login(username, password);
        }

        public void TakeAndSaveScreenShotByPath(string path)
        {
            try
            {
                Screenshot image = ((ITakesScreenshot)_webDriver).GetScreenshot();
                image.SaveAsFile(String.Concat(path, "\\Screenshot.png"), ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception("Screenshot hasn't been saved!", e);
            }

        }

        private void CreateDirectoryIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                throw new IOException("Directory hasn't been created!", ioex);
            }
        }

        #endregion
    }
}