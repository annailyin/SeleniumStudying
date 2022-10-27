﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPracticeClassLibrary.Extensions
{
    public static class WebDriverExtensions
    {
        public static void WaitUntilPageIsLoaded(this IWebDriver webDriver, string pageUrl, TimeSpan timeout)
        {
            var wait = new WebDriverWait(webDriver, timeout);
            if (!wait.Until(driver => driver.Url.StartsWith(pageUrl)))
            {
                throw new Exception("Failed to wait until page is loaded.");
            }
        }
        public static IWebDriver WaitForFrameAndSwitchToIt(this IWebDriver webDriver, By selector, TimeSpan timeout)
        {
            var wait = new WebDriverWait(webDriver, timeout);
            return wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(selector));
        }

        public static IWebElement WaitForClickableElement(this IWebDriver webDriver, By selector, TimeSpan timeout)
        {
            var wait = new WebDriverWait(webDriver, timeout);
            return wait.Until(ExpectedConditions.ElementToBeClickable(selector));
        }

        public static IWebElement WaitForVisibleElement(this IWebDriver webDriver, By selector, TimeSpan timeout)
        {
            var wait = new WebDriverWait(webDriver, timeout);
            return wait.Until(ExpectedConditions.ElementIsVisible(selector));
        }

        public static bool WaitForInvisibleElement(this IWebDriver webDriver, By selector, TimeSpan timeout)
        {
            var wait = new WebDriverWait(webDriver, timeout);
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(selector));
        }
    }
}
