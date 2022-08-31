using System;
using OpenQA.Selenium;

namespace MailRuClassLibrary
{
    public class MailRuHomePage : WebPageBase
    {
        public static readonly By LoginButtonSelector = By.XPath("//button[@class='ph-login svelte-1hiqrvn']");
        public static readonly By IframeSelector = By.CssSelector("iframe.ag-popup__frame__layout__iframe");
        public static readonly By UsernameInputSelector = By.XPath("//input[@name='username']");
        public static readonly By PasswordSubmitButtonSelector = By.XPath("//span[contains(text(), 'Enter password')]");
        public static readonly By PasswordInputSelector = By.XPath("//input[@name='password']");
        public static readonly By SignInButtonSelector = By.XPath("//span[contains(text(), 'Sign in')]");
        
        public const string MailBoxURL = "https://e.mail.ru/inbox";

        public MailRuHomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void Login(string username, string password)
        {
            WebDriver.WaitForClickableElement(LoginButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitForFrameAndSwitchToIt(IframeSelector, DefaultTimeout);
            WebDriver.WaitForVisibleElement(UsernameInputSelector, DefaultTimeout).SendKeys(username);
            WebDriver.WaitForClickableElement(PasswordSubmitButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitForVisibleElement(PasswordInputSelector, DefaultTimeout).SendKeys(password);
            WebDriver.WaitForClickableElement(SignInButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitUntilPageIsLoaded(MailBoxURL, DefaultTimeout);
        }
    }
}
