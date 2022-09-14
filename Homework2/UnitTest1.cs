using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using YandexMailClassLibrary;

namespace Homework2
{
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
        [TestCase("https://mail.yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        [TestCase("https://mail.yandex.com/", "lesnoy.kulik@yandex.com", "567RTYfghVBN!")]
        public void Login_Logout_ValidCredentials_LoginLogoutIsSuccessfullTest(string url, string username, string password)
        {
            _webDriver.Navigate().GoToUrl(url);

            //Added Implicit waiter for WebDriver, will be overlapped with Explicit waiters
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); 

            YandexMailHomePage mailHomePage = new YandexMailHomePage(_webDriver);
            YandexMailBoxPage mailBoxPage = mailHomePage.Login(username, password);
            Thread.Sleep(3000);
            mailBoxPage.Logout();

            Assert.Pass("Login/Logout test to Yandex mailbox has passed successfully!");
        }

        [Test]
        public void MultiSelectListDemoSection_SelectAnyRandomOptions_TheseOptionsAreSelected()
        {
            _webDriver.Navigate().GoToUrl("https://demo.seleniumeasy.com/basic-select-dropdown-demo.html");

            SelectElement multiSelect = new SelectElement(_webDriver.FindElement(By.Id("multi-select")));
            List<int> selectItemIndexes = Enumerable.Range(0, multiSelect.Options.Count).TakeRandomly(3).ToList();

            foreach (int index in selectItemIndexes)
            {
                multiSelect.SelectByIndex(index);
            }

            bool requiredOptionsAreSelected = selectItemIndexes
                .Select(index => multiSelect.Options.ElementAt(index))
                .All(el => el.Selected);

            Assert.IsTrue(requiredOptionsAreSelected);
        }
    }
}