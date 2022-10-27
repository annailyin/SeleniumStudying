using AutomationPracticeClassLibrary.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static readonly By WishlistNameInputSelector = By.Id("name");
        public static readonly By SaveWishlistSelector = By.Id("submitWishlist");
        public static readonly By BestSellersLinkSelector = By.LinkText("Best sellers");
        public static readonly By AddToCartButtonSelector = By.Id("add_to_cart");
        public static readonly By ContinueShoppingButton = By.ClassName("continue");
        public static readonly By IframeSelector = By.CssSelector("iframe.fancybox-iframe");
        public static readonly By BestSellersProductsSelector = By.CssSelector("a[href *='id_product'][class='product_img_link']");
        public static readonly By PriceOfProductSelector = By.Id("our_price_display");
        public static readonly By ShoppingCartSelector = By.XPath("//a[@title='View my shopping cart']");
        public static readonly By CartSummaryTableSelector = By.Id("cart_summary");
        public static readonly By ProductsInCartSummarySelector = By.CssSelector("td.cart_product");
        public static readonly By IconTrashSelector = By.ClassName("icon-trash");
        public static readonly By TotalShippingSelector = By.Id("total_shipping");
        public static readonly By TotalTaxSelector = By.Id("total_tax");
        public static readonly By TotalPriceSelector = By.Id("total_price");

        public AutomationPracticeMyAccountPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string AddProductToAutoCreatedWishlist()        
        {
            WebDriver.WaitForClickableElement(MyWishlistsButtonSelector, DefaultTimeout).Click();

            if (IsElementPresent(MyWishlistsTableSelector))
            {
                ICollection<IWebElement> removeIcons = WebDriver.FindElements(RemoveWishlistSelector);
                foreach (var _ in removeIcons)
                {
                    WebDriver.WaitForClickableElement(RemoveWishlistSelector, DefaultTimeout).Click();
                    WebDriver.SwitchTo().Alert().Accept();
                }
                WebDriver.WaitForInvisibleElement(MyWishlistsTableSelector, DefaultTimeout);
            }

            return AddRandomProductToWishlist(WebDriver.FindElements(ProductsSelector));
        }

        public string AddProductToManuallyCreatedWishlist(string name)
        {
            WebDriver.WaitForClickableElement(MyWishlistsButtonSelector, DefaultTimeout).Click();
            WebDriver.WaitForVisibleElement(WishlistNameInputSelector, DefaultTimeout).SendKeys(name);
            WebDriver.WaitForClickableElement(SaveWishlistSelector, DefaultTimeout).Click();

            return AddRandomProductToWishlist(WebDriver.FindElements(ProductsSelector));
        }

        public bool DifferentProductsAreAddedToCart(int count)
        {
            WebDriver.WaitForClickableElement(BestSellersLinkSelector, DefaultTimeout).Click();
            IList<IWebElement> products = WebDriver.FindElements(BestSellersProductsSelector);
            var totalPrice = 0m;

            foreach (var product in products.Randomize().Take(count))
            {
                product.Click();
                WebDriver.WaitForFrameAndSwitchToIt(IframeSelector, DefaultTimeout);
                totalPrice += GetValueWithoutCurrencySign(PriceOfProductSelector);
                WebDriver.WaitForClickableElement(AddToCartButtonSelector, DefaultTimeout).Click();
                WebDriver.WaitForClickableElement(ContinueShoppingButton, DefaultTimeout).Click();
            }

            WebDriver.WaitForClickableElement(ShoppingCartSelector, DefaultTimeout).Click();
            WebDriver.WaitForVisibleElement(CartSummaryTableSelector, DefaultTimeout);

            var productsCount = WebDriver.FindElements(ProductsInCartSummarySelector).Count;
            totalPrice += GetValueWithoutCurrencySign(TotalShippingSelector) + GetValueWithoutCurrencySign(TotalTaxSelector);
            EmptyProductsCart(productsCount);

            return (productsCount == count) && totalPrice.Equals(GetValueWithoutCurrencySign(TotalPriceSelector));
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

        private void EmptyProductsCart(int count)
        {
            for (var i = 0; i < count; i++)
            {
                WebDriver.WaitForClickableElement(IconTrashSelector, DefaultTimeout).Click();
            }
        }

        private decimal GetValueWithoutCurrencySign(By by)
        {
            string s = WebDriver.WaitForVisibleElement(by, DefaultTimeout).Text;
            return Convert.ToDecimal(s.Substring(1));
        }

        private string AddRandomProductToWishlist(IList<IWebElement> products)
        {
            products.Randomize().Take(1).ToList().ElementAt(0).Click();                 
            WebDriver.WaitForClickableElement(WishlistButtonSelector, DefaultTimeout).Click();

            return WebDriver.WaitForVisibleElement(ConfirmationBoxSelector, DefaultTimeout).Text;
        }

        #endregion
    }
}
