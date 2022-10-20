using AutomationPracticeClassLibrary;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPracticeFinalTask
{
    [TestFixture]
    public class Tests
    {
        private const string _baseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        private WebDriver _webDriver;

        [SetUp]
        public void Setup()
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
        [TestCase("udod.udodovich@yandex.com")]
        public void CreateAutomationPracticeAccount_UserEntersValidData_AccountCreatedSuccessfullTest(string emailAddress)
        {
            _webDriver.Navigate().GoToUrl(_baseURL);

            AutomationPracticeStartPage startPage = new AutomationPracticeStartPage(_webDriver);
            AutomationPracticeCreateAccountPage createAccountPage = startPage.CreateAccount(emailAddress);
        }
    }
}