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
        BaseTest<ChromeDriver> baseTestChrome;
        BaseTest<FirefoxDriver> baseTestFirefox;
        BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        // Web page variables.
        InterpOfferPage pageChrome;
        InterpOfferPage pageFirefox;
        InterpOfferPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            baseTestChrome = new BaseTest<ChromeDriver>(InterpOfferPage.Url);
            baseTestFirefox = new BaseTest<FirefoxDriver>(InterpOfferPage.Url);
            baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(InterpOfferPage.Url);
            pageChrome = new InterpOfferPage(baseTestChrome.WebDriver);
            pageFirefox = new InterpOfferPage(baseTestFirefox.WebDriver);
            pageInternetExplorer = new InterpOfferPage(baseTestInternetExplorer.WebDriver);
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
        public void TestActivityBoxEmpty()
        {
            pageChrome.CheckActivityBox(ActivityBoxCheck.IsEmpty);
            pageFirefox.CheckActivityBox(ActivityBoxCheck.IsEmpty);
            pageInternetExplorer.CheckActivityBox(ActivityBoxCheck.IsEmpty);
        }

        /// <summary>
        /// Test if 'Activity type' dropbox is enabled.
        /// </summary>
        [Test]
        public void TestActivityBoxEnabled()
        {
            pageChrome.CheckActivityBox(ActivityBoxCheck.IsEnabled);
            pageFirefox.CheckActivityBox(ActivityBoxCheck.IsEnabled);
            pageInternetExplorer.CheckActivityBox(ActivityBoxCheck.IsEnabled);
        }
    }
}
