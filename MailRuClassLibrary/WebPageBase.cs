using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace MailRuClassLibrary
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
