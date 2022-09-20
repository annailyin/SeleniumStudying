using Core.Domain;
using Core.Extensions;
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

        //Tasks #1-4
        [Test]
        [TestCase("https://mail.yandex.com/", "udod.udodovich@yandex.com", "cxzASDewq123567")]
        [TestCase("https://mail.yandex.com/", "lesnoy.kulik@yandex.com", "567RTYfghVBN!")]
        public void MailBoxLoginLogout_UserEntersValidCredentials_LoginLogoutIsSuccessfullTest(string url, string username, string password)
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

        //Task #5
        [Test]
        public void MultiSelectListDemoSection_SelectAnyRandomOptions_TheseOptionsAreSelected()
        {
            var multiSelectLocator = By.Id("multi-select");
            _webDriver.Navigate().GoToUrl("https://demo.seleniumeasy.com/basic-select-dropdown-demo.html");

            SelectElement multiSelect = new SelectElement(_webDriver.FindElement(multiSelectLocator));
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

        //Task #6
        [Test]
        public void JavaScriptAlertBox_UserPressesClickMeButtonThenOkButtonInAlert_CorrectMessageIsDisplayed()
        {
            IAlert alert = UserPressesClickMeButtonInJavaScriptAlertBox();
            String actualAlertMessage = alert.Text;
            alert.Accept();

            Assert.AreEqual("I am an alert box!", actualAlertMessage);
        }

        //Task #6
        [Test]
        public void JavaScriptConfirmBox_UserPressesClickMeButtonThenOkButtonInAlert_CorrectMessageIsDisplayed()
        {
            var confirmDemoSelector = By.Id("confirm-demo");
            UserPressesClickMeButtonInJavaScriptConfirmBox().Accept();

            Assert.AreEqual("You pressed OK!", _webDriver.FindElement(confirmDemoSelector).Text);
        }

        //Task #6
        [Test]
        public void JavaScriptConfirmBox_UserPressesClickMeButtonThenCancelButtonInAlert_CorrectMessageIsDisplayed()
        {
            var confirmDemoSelector = By.Id("confirm-demo");
            UserPressesClickMeButtonInJavaScriptConfirmBox().Dismiss();

            Assert.AreEqual("You pressed Cancel!", _webDriver.FindElement(confirmDemoSelector).Text);
        }

        //Task #7
        [Test]
        public void GetRandomUser_UserPressesGetNewUserButton_UserIconIsLoaded()
        {
            var buttonSelector = By.Id("save");
            var imageSelector = By.CssSelector("#loading img[src^='https://randomuser.me/api/portraits']");

            _webDriver
                .Navigate()
                .GoToUrl("https://demo.seleniumeasy.com/dynamic-data-loading-demo.html");

            _webDriver
                .FindElement(buttonSelector)
                .Click();

            _webDriver.WaitUntilImageIsFullyLoaded(imageSelector, TimeSpan.FromSeconds(60));
        }

        //Task #8
        [Test]
        public void ProgressBarDemo_UserClicksDownloadButtonAndRefreshesPage_PageRefreshesAfterProgressBarShowsSpecificPercentage()
        {
            var buttonSelector = By.Id("cricle-btn");
            var percentTextSelector = By.CssSelector(".percenttext");
            var thresholdValue = 50;

            _webDriver
                .Navigate()
                .GoToUrl("https://demo.seleniumeasy.com/bootstrap-download-progress-demo.html");

            _webDriver
                .FindElement(buttonSelector)
                .Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));
            wait.Until(wd => {
                var element = wd.FindElement(percentTextSelector);
                return element != null && double.TryParse(element.Text.TrimEnd('%'), out var value) && value >= thresholdValue;
            });

            _webDriver.Navigate().Refresh();
        }

        //Task #9
        [Test]
        [TestCase(30, 100000)]
        public void TableSortAndSearchDemo_UserChoosesEntriesCount_GetListOfCustomObjects(int minAge, decimal maxSalary)
        {
            _webDriver
                .Navigate()
                .GoToUrl("https://demo.seleniumeasy.com/table-sort-search-demo.html");

            var employees = GetEmployees(minAge, maxSalary);

            Assert.AreEqual(3, employees.Count);
        }

        #region Private

        private IAlert UserPressesClickMeButtonInJavaScriptConfirmBox()
        {
            var buttonSelector = By.XPath("//button[contains(@class, 'btn-lg') and @onclick='myConfirmFunction()']");

            _webDriver
                .Navigate()
                .GoToUrl("https://demo.seleniumeasy.com/javascript-alert-box-demo.html");

            _webDriver
                .FindElement(buttonSelector)
                .Click();

            return(_webDriver.SwitchTo().Alert());
        }

        private IAlert UserPressesClickMeButtonInJavaScriptAlertBox()
        {
            var buttonSelector = By.XPath("//button[contains(@class, 'btn-default') and @onclick='myAlertFunction()']");

            _webDriver
                .Navigate()
                .GoToUrl("https://demo.seleniumeasy.com/javascript-alert-box-demo.html");

            _webDriver
                .FindElement(buttonSelector)
                .Click();

            return (_webDriver.SwitchTo().Alert());
        }

        private List<Employee> GetEmployees(int minAge, decimal maxSalary)
        {
            By nextButtonSelector = By.CssSelector(".paginate_button.next");
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                employees.AddRange(GetEmployeesFromPage(minAge, maxSalary));
                IWebElement nextButton = _webDriver.FindElement(nextButtonSelector);
                if (!nextButton.GetAttribute("class").Contains("disabled"))
                {
                    nextButton.Click();
                }
                else break;
            }

            return employees;
        }

        private List<Employee> GetEmployeesFromPage(int minAge, decimal maxSalary)
        {
            const int NameColumnIndex = 0;
            const int PositionColumnIndex = 1;
            const int OfficeColumnIndex = 2;
            const int AgeColumnIndex = 3;
            const int SalaryColumnIndex = 5;

            By tableSelector = By.CssSelector("#example");

            IWebElement table = _webDriver.FindElement(tableSelector);
            IWebElement headerRow = table.FindElement(By.CssSelector("thead tr"));
            ICollection<IWebElement> rows = table.FindElements(By.CssSelector("tbody tr"));

            List<Employee> employees = rows
                .Select(row => row.FindElements(By.TagName("td")))
                .Where(cells =>
                {
                    int age = int.Parse(cells[AgeColumnIndex].Text);
                    decimal salary = decimal.Parse(cells[SalaryColumnIndex].Text.Trim('$', '/', 'y'));
                    return age > minAge && salary <= maxSalary;
                })
                .Select(cells => new Employee
                {
                    Name = cells[NameColumnIndex].Text,
                    Position = cells[PositionColumnIndex].Text,
                    Office = cells[OfficeColumnIndex].Text
                })
                .ToList();

            return employees;
        }

        #endregion
    }
}