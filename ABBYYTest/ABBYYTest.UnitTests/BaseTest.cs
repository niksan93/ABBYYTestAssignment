using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using ABBYYTest;

namespace ABBYYTest.UnitTests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class BaseTest<TIWebDriver> where TIWebDriver : IWebDriver, new()
    {
        protected enum PageInfo { ContactInfo, LangSwitcherExistence, LangSwitcherElements }
        static IWebDriver webDriver;

        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="url"></param>
        public static void Initialize(string url)
        {
            webDriver = new TIWebDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
            WebDriver = webDriver;
        }

        /// <summary>
        /// IWebDriver property.
        /// </summary>
        public static IWebDriver WebDriver
        {
            get;
            set;
        }

        /// <summary>
        /// Quit the current IWebDriver.
        /// </summary>
        public static void Dispose()
        {
            webDriver.Quit();
        }

        /// <summary>
        /// Base method for testing and reacting to contact information and language switch on page.
        /// </summary>
        /// <param name="action">Delegate method.</param>
        /// <param name="info">PageInfo to react with respective message in Exception.</param>
        protected void BaseTestInfo(Action action, PageInfo info)
        {
            try
            {
                action();
            }
            catch (AssertionException)
            {
                if (info == PageInfo.ContactInfo)
                    BasePage.TakeScreenshot(ScreenShotType.ContactInfo, webDriver);
                else
                    BasePage.TakeScreenshot(ScreenShotType.LanguageChange, webDriver);
                webDriver.Quit();
                string messageStart = "";
                if (info == PageInfo.ContactInfo)
                    messageStart = "Phone number is not correct on page ";
                if (info == PageInfo.LangSwitcherExistence)
                    messageStart = "There is no language switcher element on page ";
                if (info == PageInfo.LangSwitcherElements)
                    messageStart = "Unexpected languages in language drop box ";
                string message = string.Concat(messageStart, webDriver.Url);
                throw new AssertionException(message);
            }
        }
    }
}
