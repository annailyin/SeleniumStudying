using AutomationPracticeClassLibrary.Extensions;
using AutomationPracticeClassLibrary.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeClassLibrary.Pages
{
    public class AutomationPracticeCreateAccountPage : WebPageBase
    {
        public const string BaseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account#account-creation";

        public static readonly By FirstNameInputSelector = By.Id("customer_firstname");
        public static readonly By LastNameInputSelector = By.Id("customer_lastname");
        public static readonly By PasswordInputSelector = By.Id("passwd");
        public static readonly By AddressInputSelector = By.Id("address1");
        public static readonly By CityInputSelector = By.Id("city");
        public static readonly By StateMultiSelector = By.Id("id_state");
        public static readonly By StateDropdownSelector = By.Id("uniform-id_state");
        public static readonly By ZipPostalCodeSelector = By.Id("postcode");
        public static readonly By CountryMultiSelector = By.Id("id_country");
        public static readonly By CountryDropdownSelector = By.Id("id_country");
        public static readonly By MobilePhoneCodeSelector = By.Id("phone_mobile");
        public static readonly By AddressAliasInputSelector = By.Id("alias");
        public static readonly By SubmitAccountButtonSelector = By.Id("submitAccount");

        public AutomationPracticeCreateAccountPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public AutomationPracticeMyAccountPage CreateAccount(User account)
        {
            SelectElement stateMultiSelect = new SelectElement(WebDriver.FindElement(StateMultiSelector));
            SelectElement countryMultiSelect = new SelectElement(WebDriver.FindElement(CountryMultiSelector));      

            WebDriver.WaitForVisibleElement(FirstNameInputSelector, DefaultTimeout).SendKeys(account.FirstName);
            WebDriver.WaitForVisibleElement(LastNameInputSelector, DefaultTimeout).SendKeys(account.LastName);
            WebDriver.WaitForVisibleElement(LastNameInputSelector, DefaultTimeout).SendKeys(account.LastName);
            WebDriver.WaitForVisibleElement(PasswordInputSelector, DefaultTimeout).SendKeys(account.Password);
            WebDriver.WaitForVisibleElement(AddressInputSelector, DefaultTimeout).SendKeys(account.Address);
            WebDriver.WaitForVisibleElement(CityInputSelector, DefaultTimeout).SendKeys(account.City);
            stateMultiSelect.SelectByText(account.State);
            WebDriver.WaitForVisibleElement(ZipPostalCodeSelector, DefaultTimeout).SendKeys(account.ZipPostalCode);
            countryMultiSelect.SelectByText(account.Country);
            WebDriver.WaitForVisibleElement(MobilePhoneCodeSelector, DefaultTimeout).SendKeys(account.MobilePhone);
            WebDriver.WaitForVisibleElement(AddressAliasInputSelector, DefaultTimeout).Clear();
            WebDriver.WaitForVisibleElement(AddressAliasInputSelector, DefaultTimeout).SendKeys(account.AddressAlias);
            WebDriver.WaitForClickableElement(SubmitAccountButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(AutomationPracticeMyAccountPage.BaseURL, DefaultTimeout);

            return new AutomationPracticeMyAccountPage(WebDriver);
        }
    }
}
