using AutomationPracticeClassLibrary.Entities;
using AutomationPracticeClassLibrary.Extensions;
using OpenQA.Selenium;

namespace AutomationPracticeClassLibrary.Pages
{
    public class AutomationPracticeStartPage : WebPageBase
    {
        public static readonly By EmailAddressInputSelector = By.Id("email_create");
        public static readonly By CreateAccountSubmitButtonSelector = By.Id("SubmitCreate");
        public static readonly By EmailAddressInputSelectorSelector2 = By.Id("email");
        public static readonly By PasswordInputSelectorSelector2 = By.Id("passwd");
        public static readonly By SubmitLoginButtonSelector = By.Id("SubmitLogin");

        public AutomationPracticeStartPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public AutomationPracticeCreateAccountPage GoToCreateAccountPage(User account)
        {
            WebDriver.WaitForVisibleElement(EmailAddressInputSelector, DefaultTimeout).SendKeys(account.EmailAddress);
            WebDriver.WaitForClickableElement(CreateAccountSubmitButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(AutomationPracticeCreateAccountPage.BaseURL, DefaultTimeout);

            return new AutomationPracticeCreateAccountPage(WebDriver);
        }

        public AutomationPracticeMyAccountPage Login(User account)
        {
            WebDriver.WaitForVisibleElement(EmailAddressInputSelectorSelector2, DefaultTimeout).SendKeys(account.EmailAddress);
            WebDriver.WaitForVisibleElement(PasswordInputSelectorSelector2, DefaultTimeout).SendKeys(account.Password);
            WebDriver.WaitForClickableElement(SubmitLoginButtonSelector, DefaultTimeout).Click();

            return new AutomationPracticeMyAccountPage(WebDriver);
        }
    }
}
