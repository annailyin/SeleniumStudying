using AutomationPracticeClassLibrary;
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

        private AutomationPracticeAccount _account;

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
        [TestCase("udod.udodovich@yandex.com")]
        public void CreateAutomationPracticeAccount_UserEntersValidData_AccountCreatedSuccessfullTest(string emailAddress)
        {
            _webDriver.Navigate().GoToUrl(_baseURL);

            AutomationPracticeStartPage startPage = new AutomationPracticeStartPage(_webDriver);
            AutomationPracticeCreateAccountPage createAccountPage = startPage.GoToCreateAccountPage(_account);
            createAccountPage.CreateAccount(_account);
        }

        #region private

        public void ReadJsonDataForAutomationPracticeAccount(string fileName)
        {
            _account = new AutomationPracticeAccount();

            string jsonString = File.ReadAllText(fileName);
            _account = JsonSerializer.Deserialize<AutomationPracticeAccount>(jsonString);
        }

        #endregion
    }
}