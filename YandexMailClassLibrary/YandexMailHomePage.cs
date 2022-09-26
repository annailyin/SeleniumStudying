using Extensions;
using OpenQA.Selenium;

namespace YandexMailClassLibrary
{
    public class YandexMailHomePage : WebPageBase
    {
        public const string BaseURL = "https://mail.yandex.com/";

        public static readonly By LoginButtonSelector = By.XPath("//button[contains(@class,'PSHeader-NoLoginButton')]");
        public static readonly By UsernameInputSelector = By.XPath("//input[@id='passp-field-login']");
        public static readonly By UsernameSubmitButtonSelector = By.XPath("//button[contains(@class, 'Button2_type_submit')]");
        public static readonly By PasswordInputSelector = By.XPath("//input[@id='passp-field-passwd']");
        public static readonly By SignInButtonSelector = By.XPath("//button[@id='passp:sign-in']");

        public YandexMailHomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public YandexMailBoxPage Login(string username, string password)
        {
            WebDriver.WaitForClickableElement(LoginButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitForVisibleElement(UsernameInputSelector, DefaultTimeout).SendKeys(username);
            WebDriver.WaitForClickableElement(UsernameSubmitButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitForVisibleElement(PasswordInputSelector, DefaultTimeout).SendKeys(password);
            WebDriver.WaitForClickableElement(SignInButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(YandexMailBoxPage.BaseURL, DefaultTimeout);

            return new YandexMailBoxPage(WebDriver);
        }
    }
}
