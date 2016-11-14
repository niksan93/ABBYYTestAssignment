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
    public class UnitTestCalcPage
    {
        // BaseTest variables for browsers.
        private static BaseTest<ChromeDriver> baseTestChrome;
        private static BaseTest<FirefoxDriver> baseTestFirefox;
        private static BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        // Web page variables.
        private static CalculatorPage pageChrome;
        private static CalculatorPage pageFirefox;
        private static CalculatorPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void setUpMainPage()
        {
            baseTestChrome = new BaseTest<ChromeDriver>(CalculatorPage.url);
            baseTestFirefox = new BaseTest<FirefoxDriver>(CalculatorPage.url);
            baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(CalculatorPage.url);
            pageChrome = new CalculatorPage(baseTestChrome.webDriver);
            pageFirefox = new CalculatorPage(baseTestFirefox.webDriver);
            pageInternetExplorer = new CalculatorPage(baseTestInternetExplorer.webDriver);
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
        /// Test 'From language' dropbox for emptiness
        /// </summary>
        [Test]
        public static void testfromLangDropBox()
        {
            pageChrome.checkLangDropboxEmpty(DroboxType.From);
            pageFirefox.checkLangDropboxEmpty(DroboxType.From);
            pageInternetExplorer.checkLangDropboxEmpty(DroboxType.From);
        }

        /// <summary>
        /// Test 'From language' dropbox to have 'Русский' as an option.
        /// </summary>
        [Test]
        public static void testFromLangOptions()
        {
            pageChrome.checkLangOptions(DroboxType.From);
            pageFirefox.checkLangOptions(DroboxType.From);
            pageInternetExplorer.checkLangOptions(DroboxType.From);
        }

        /// <summary>
        /// Test 'To language' dropbox for emptiness.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public static void testToLangDropBox()
        {
            pageChrome.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageChrome.checkLangDropboxEmpty(DroboxType.To);

            pageFirefox.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageFirefox.checkLangDropboxEmpty(DroboxType.To);

            pageInternetExplorer.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageInternetExplorer.checkLangDropboxEmpty(DroboxType.To);
        }

        /// <summary>
        /// Test 'To language' dropbox for 'Английский' as an option.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public static void testToLangOptions()
        {
            pageChrome.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageChrome.checkLangOptions(DroboxType.To);

            pageFirefox.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageFirefox.checkLangOptions(DroboxType.To);

            pageInternetExplorer.checkLangOptions(DroboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageInternetExplorer.checkLangOptions(DroboxType.To);
        }
    }
}
