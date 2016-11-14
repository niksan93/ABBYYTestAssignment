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
    public class UnitTestMainPage
    {
        // BaseTest variables for browsers.
        private static BaseTest<ChromeDriver> baseTestChrome;
        private static BaseTest<FirefoxDriver> baseTestFirefox;
        private static BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        // Web page variables.
        private static MainPage pageChrome;
        private static MainPage pageFirefox;
        private static MainPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void setUpMainPage()
        {
           // try
            //{
                baseTestChrome = new BaseTest<ChromeDriver>(MainPage.url);
                baseTestFirefox = new BaseTest<FirefoxDriver>(MainPage.url);
                baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(MainPage.url);
            //}
            //catch (Exception e)
           // {
            //    Dispose();
            //    throw new Exception("Error loading page");
           // }
            pageChrome = new MainPage(baseTestChrome.webDriver);
            pageFirefox = new MainPage(baseTestFirefox.webDriver);
            pageInternetExplorer = new MainPage(baseTestInternetExplorer.webDriver);
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
        /// Test that correct images appear on pressing certain buttons in the left menu.
        /// </summary>
        [Test]
        public static void testImageDisplayed()
        {
            pageChrome.checkImage();
            pageFirefox.checkImage();
            pageInternetExplorer.checkImage();
        }


        /// <summary>
        /// Test that positions of current image and the rest on the base image are different.
        /// I.e. Test that correct images appear on pressing certain buttons in the left menu.
        /// </summary>
        [Test]
        public static void testImagePosition()
        {
            pageChrome.checkImagePosition();
            pageFirefox.checkImagePosition();
            pageInternetExplorer.checkImagePosition();
        }
    }
}
