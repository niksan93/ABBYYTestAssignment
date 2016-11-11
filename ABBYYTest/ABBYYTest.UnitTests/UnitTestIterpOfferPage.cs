using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using NUnit.Framework;
using ABBYYTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace ABBYYTest.UnitTests
{
    [TestFixture]
    public class UnitTestIterpOfferPage
    {
        private static InterpOfferPage interpPage;
        private static IWebDriver driver;
        /// <summary>
        /// Chrome test.
        /// </summary>
        [Test]
        public void chromeInterpPageTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            testActivityBox(driver);
            driver.Quit();
        }
        /// <summary>
        /// Firefox test.
        /// </summary>
        [Test]
        public void firefoxInterpPageTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            testActivityBox(driver);
            driver.Quit();
        }
        /// <summary>
        /// Internet explorer test.
        /// </summary>
        [Test]
        public void ieInterpPageTest()
        {
            driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            testActivityBox(driver);
            driver.Quit();
        }

        /// <summary>
        /// Test if 'activity type' dropbox is not empty and activities can be chosen.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        static void testActivityBox(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(InterpOfferPage.url);
            interpPage = new InterpOfferPage(driver);
            IWebElement activityBox = null;
            IList<IWebElement> activityList = null;
            checkActivityEnabled(ref activityBox, ref activityList);
            checkActivityBox(activityBox, activityList);
            AdditionalFunc.checkPhoneText(driver);
            AdditionalFunc.checkLangSwitcherExistence(driver);
            AdditionalFunc.checkLangSwitcherElements(driver);
        }

        /// <summary>
        /// Check if 'activity type' dropbox is not empty.
        /// </summary>
        /// <param name="activityBox">IWebElement of dropbox</param>
        /// <param name="activityOptions">Ilist of options of dropbox</param>
        static void checkActivityBox(IWebElement activityBox, IList<IWebElement> activityOptions)
        {
            try
            {
                activityBox = interpPage.getActivityBoxElement();
                activityBox.Click();
                activityOptions = interpPage.getActivityBoxOptions();
                Assert.IsTrue(activityOptions.Count >= 1);
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShotInterpPage(driver);
                driver.Quit();
                throw new AssertionException("'Activity type' dropbox is empty");
            }
        }

        /// <summary>
        /// Check if activity dropBox is enabled.
        /// </summary>
        /// <param name="activityBox">IWebElement of dropbox</param>
        /// <param name="activityOptions">Ilist of options of dropbox</param>
        static void checkActivityEnabled(ref IWebElement activityBox, ref IList<IWebElement> activityOptions)
        {
            try
            {
                activityBox = interpPage.getActivityBoxElement();
                activityBox.Click();
                activityOptions = interpPage.getActivityBoxOptions();
                Assert.IsTrue(activityBox.Enabled);
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShotInterpPage(driver);
                driver.Quit();
                throw new AssertionException("'Activity type' dropbox is disabled. Not possible to choose activity.");
            }
        }
    }
}
