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
        BaseTest<ChromeDriver> baseTestChrome;
        BaseTest<FirefoxDriver> baseTestFirefox;
        BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        // Web page variables.
        CalculatorPage pageChrome;
        CalculatorPage pageFirefox;
        CalculatorPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            baseTestChrome = new BaseTest<ChromeDriver>(CalculatorPage.Url);
            baseTestFirefox = new BaseTest<FirefoxDriver>(CalculatorPage.Url);
            baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(CalculatorPage.Url);
            pageChrome = new CalculatorPage(baseTestChrome.WebDriver);
            pageFirefox = new CalculatorPage(baseTestFirefox.WebDriver);
            pageInternetExplorer = new CalculatorPage(baseTestInternetExplorer.WebDriver);
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
        public void TestfromLangDropBox()
        {
            pageChrome.CheckLangDropboxEmpty(DropboxType.From);
            pageFirefox.CheckLangDropboxEmpty(DropboxType.From);
            pageInternetExplorer.CheckLangDropboxEmpty(DropboxType.From);
        }

        /// <summary>
        /// Test 'From language' dropbox to have 'Русский' as an option.
        /// </summary>
        [Test]
        public void TestFromLangOptions()
        {
            pageChrome.CheckLangOptions(DropboxType.From);
            pageFirefox.CheckLangOptions(DropboxType.From);
            pageInternetExplorer.CheckLangOptions(DropboxType.From);
        }

        /// <summary>
        /// Test 'To language' dropbox for emptiness.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public void TestToLangDropBox()
        {
            pageChrome.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageChrome.CheckLangDropboxEmpty(DropboxType.To);

            pageFirefox.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageFirefox.CheckLangDropboxEmpty(DropboxType.To);

            pageInternetExplorer.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageInternetExplorer.CheckLangDropboxEmpty(DropboxType.To);
        }

        /// <summary>
        /// Test 'To language' dropbox for 'Английский' as an option.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public void TestToLangOptions()
        {
            pageChrome.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageChrome.CheckLangOptions(DropboxType.To);

            pageFirefox.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageFirefox.CheckLangOptions(DropboxType.To);

            pageInternetExplorer.CheckLangOptions(DropboxType.From);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            pageInternetExplorer.CheckLangOptions(DropboxType.To);
        }
    }
}
