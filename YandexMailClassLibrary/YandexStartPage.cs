using Extensions;
using OpenQA.Selenium;

namespace YandexMailClassLibrary
{
    public class YandexStartPage : WebPageBase
    {
        public static readonly By LoginLinkSelector = By.XPath("//div[contains(@class, 'b-inline') and text() = 'Log in']");

        public YandexStartPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public YandexMailHomePage GoToMailHomePage()
        {
            WebDriver.WaitForClickableElement(LoginLinkSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(YandexMailHomePage.BaseURL, DefaultTimeout);

            return new YandexMailHomePage(WebDriver);
        }
    }
}
