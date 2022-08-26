using MailRuClassLibrary;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homework1
{
    public class Tests
    {
        const string Username = "udodudodovich@mail.ru";
        const string Password = "cxzASDewq123567";
        const string Url = "https://mail.ru/";

        By MailAddressSelector = By.XPath("//span[@class='ph-project__user-name svelte-1hiqrvn']");

        [Test]
        public void Test1()
        {
            WebDriver driver = new ChromeDriver { Url = Url };
            driver.Manage().Window.Maximize();

            MailRuHomePage homePage = new MailRuHomePage(driver);
            homePage.Login(Username, Password);

            var mailAddress = driver.FindElement(MailAddressSelector);
            var value = mailAddress.Text;
            Assert.AreEqual(Username, value);

            driver.Close();
            driver.Dispose();
        }
    }
}