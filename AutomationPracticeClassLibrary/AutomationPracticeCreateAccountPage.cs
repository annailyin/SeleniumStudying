using OpenQA.Selenium;

namespace AutomationPracticeClassLibrary
{
    public class AutomationPracticeCreateAccountPage : WebPageBase
    {
        public const string BaseURL = "http://automationpractice.com/index.php?controller=authentication&back=my-account#account-creation";

        public AutomationPracticeCreateAccountPage(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
