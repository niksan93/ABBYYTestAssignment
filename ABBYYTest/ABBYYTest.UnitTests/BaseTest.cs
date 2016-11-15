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

        public BaseTest()
        {

        }
        public BaseTest(string url)
        {
            webDriver = new TIWebDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
            WebDriver = webDriver;
           // BasePage.CheckPhoneText(webDriver);
           // BasePage.CheckLangSwitcherExistence(webDriver);
           // BasePage.CheckLangSwitcherElements(webDriver);
        }

        public static void Initialize(string url)
        {
            webDriver = new TIWebDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
            Driver = webDriver;
        }

        public static IWebDriver Driver
        {
            get;
            set;
        }

        /// <summary>
        /// IWebDriver property
        /// </summary>
        public IWebDriver WebDriver
        {
            get;
            set;
        }

        public static void Clean()
        {
            webDriver.Quit();
        }
        /// <summary>
        /// Quit the current IWebDriver.
        /// </summary>
        public void Dispose()
        {
            webDriver.Quit();
        }

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
        
        /*protected void BaseTestContactInfo(Action action)//, IWebDriver driver)
        {
            try
            {
                action();
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.ContactInfo, webDriver);
                webDriver.Quit();
                throw new AssertionException("Phone number is not correct on page" + webDriver.Url);
            }
        }

        protected void BaseTestLangSwitcherExistence(Action action)
        {
            try
            {
                action();
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.LanguageChange, webDriver);
                webDriver.Quit();
                throw new AssertionException("There is no language switcher element on page" + webDriver.Url);
            }
        }

        protected void BaseTestLangSwitcherElements(Action action)
        {
            try
            {
                action();
            }
            catch (AssertionException)
            {
                //driver.FindElement(langSwitcherLocator).Click();
                BasePage.TakeScreenshot(ScreenShotType.LanguageChange, webDriver);
                webDriver.Quit();
                throw new Exception("Unexpected languages in language drop box");
            }
        }*/

    }
}
