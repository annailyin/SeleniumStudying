using System;
using OpenQA.Selenium;

namespace YandexMailClassLibrary
{
    public abstract class WebPageBase
    {
        protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

        protected IWebDriver WebDriver { get; }

        public WebPageBase(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
    }
}
