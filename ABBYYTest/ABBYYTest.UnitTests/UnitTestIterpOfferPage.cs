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
        // BaseTest variables for browsers.
        private static BaseTest<ChromeDriver> baseTestChrome;
        private static BaseTest<FirefoxDriver> baseTestFirefox;
        private static BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        // Web page variables.
        private static InterpOfferPage pageChrome;
        private static InterpOfferPage pageFirefox;
        private static InterpOfferPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void setUpMainPage()
        {
            baseTestChrome = new BaseTest<ChromeDriver>(InterpOfferPage.url);
            baseTestFirefox = new BaseTest<FirefoxDriver>(InterpOfferPage.url);
            baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(InterpOfferPage.url);
            pageChrome = new InterpOfferPage(baseTestChrome.webDriver);
            pageFirefox = new InterpOfferPage(baseTestFirefox.webDriver);
            pageInternetExplorer = new InterpOfferPage(baseTestInternetExplorer.webDriver);
        }

        /// <summary>
        /// A method to be performed after all tests in this class are completed regardless of the outcome.
        /// Quits all the IWebDrivers.
        /// </summary>
        [OneTimeTearDown]
        public void Dispose()
        {
            baseTestChrome.Dispose();
            baseTestFirefox.Dispose();
            baseTestInternetExplorer.Dispose();
        }

        /// <summary>
        /// Test 'Activity type' dropbox for emptines.
        /// </summary>
        [Test]
        public static void testActivityBoxEmpty()
        {
            pageChrome.checkActivityBox(ActivityBoxCheck.IsEmpty);
            pageFirefox.checkActivityBox(ActivityBoxCheck.IsEmpty);
            pageInternetExplorer.checkActivityBox(ActivityBoxCheck.IsEmpty);
        }

        /// <summary>
        /// Test if 'Activity type' dropbox is enabled.
        /// </summary>
        [Test]
        public static void testActivityBoxEnabled()
        {
            pageChrome.checkActivityBox(ActivityBoxCheck.IsEnabled);
            pageFirefox.checkActivityBox(ActivityBoxCheck.IsEmpty);
            pageInternetExplorer.checkActivityBox(ActivityBoxCheck.IsEmpty);
        }
    }
}
