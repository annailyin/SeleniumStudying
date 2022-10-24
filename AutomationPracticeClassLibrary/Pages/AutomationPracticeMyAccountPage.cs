using AutomationPracticeClassLibrary.Extensions;
using OpenQA.Selenium;

namespace AutomationPracticeClassLibrary.Pages
{
    public class AutomationPracticeMyAccountPage: WebPageBase
    {
        public const string BaseURL = "http://automationpractice.com/index.php?controller=my-account";

        public static readonly By MyWishlistsButtonSelector = By.CssSelector(".lnk_wishlist a");

        public AutomationPracticeMyAccountPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void GoToMyWishlists()
        {
            WebDriver.WaitForClickableElement(MyWishlistsButtonSelector, DefaultTimeout).Click();
        }
    }
}
