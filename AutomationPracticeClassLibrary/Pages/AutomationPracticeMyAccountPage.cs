using AutomationPracticeClassLibrary.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AutomationPracticeClassLibrary.Pages
{
    public class AutomationPracticeMyAccountPage: WebPageBase
    {
        public const string BaseURL = "http://automationpractice.com/index.php?controller=my-account";

        public static readonly By MyWishlistsButtonSelector = By.ClassName("lnk_wishlist");
        public static readonly By MyWishlistsTableSelector = By.ClassName("table-bordered");
        public static readonly By ProductsSelector = By.CssSelector("a[href*='id_product'][class='product-name']");
        public static readonly By WishlistButtonSelector = By.Id("wishlist_button");
        public static readonly By ConfirmationBoxSelector = By.ClassName("fancybox-error");
        public static readonly By RemoveWishlistSelector = By.ClassName("icon-remove");

        public AutomationPracticeMyAccountPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string AddProductToAutoCreatedWishlist()        
        {
            WebDriver.WaitForClickableElement(MyWishlistsButtonSelector, DefaultTimeout).Click();

            if (IsElementPresent(MyWishlistsTableSelector))
            {
                WebDriver.WaitForClickableElement(RemoveWishlistSelector, DefaultTimeout).Click();
                WebDriver.SwitchTo().Alert().Accept();
                WebDriver.WaitForInvisibleElement(MyWishlistsTableSelector, DefaultTimeout);
            }

            return AddRandomProductToAutoCreatedWishlist(WebDriver.FindElements(ProductsSelector));
        }

        #region Subsidiary

        private bool IsElementPresent(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private IWebElement RandomProduct(IList<IWebElement> products)
        {
            Random r = new Random();
            int randomValue = r.Next(1, (products.Count) - 1);
            IWebElement randomProduct = products[randomValue];
            return randomProduct;
        }

        private string AddRandomProductToAutoCreatedWishlist(IList<IWebElement> products)
        {
            RandomProduct(products).Click();
            WebDriver.WaitForClickableElement(WishlistButtonSelector, DefaultTimeout).Click();

            return WebDriver.WaitForVisibleElement(ConfirmationBoxSelector, DefaultTimeout).Text;
        }

        #endregion
    }
}
