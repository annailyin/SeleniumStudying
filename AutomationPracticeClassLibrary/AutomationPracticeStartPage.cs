using AutomationPracticeClassLibrary.Extensions;
using OpenQA.Selenium;

namespace AutomationPracticeClassLibrary
{
    public class AutomationPracticeStartPage : WebPageBase
    {
        public static readonly By EmailAddressInputSelector = By.Id("email_create");
        public static readonly By CreateAccountSubmitButtonSelector = By.Id("SubmitCreate");

        public AutomationPracticeStartPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public AutomationPracticeCreateAccountPage CreateAccount(string emailAddress)
        {
            WebDriver.WaitForVisibleElement(EmailAddressInputSelector, DefaultTimeout).SendKeys(emailAddress);
            WebDriver.WaitForClickableElement(CreateAccountSubmitButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(AutomationPracticeCreateAccountPage.BaseURL, DefaultTimeout);

            return new AutomationPracticeCreateAccountPage(WebDriver);
        }
    }
}
