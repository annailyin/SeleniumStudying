using OpenQA.Selenium;

namespace YandexMailClassLibrary
{
    public class YandexMailBoxPage : WebPageBase
    {
        public const string BaseURL = "https://mail.yandex.com/";

        public static readonly By UserAccountSelector = By.XPath("//a[@href='https://passport.yandex.com']");
        public static readonly By LogoutLinkSelector = By.XPath("//span[@class='menu__text' and contains(text(),'Log out')]");

        public YandexMailBoxPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void Logout()
        {
            WebDriver.WaitForClickableElement(UserAccountSelector, DefaultTimeout).Click();
            WebDriver.WaitForClickableElement(LogoutLinkSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(YandexMailHomePage.BaseURL, DefaultTimeout);
        }
    }
}
