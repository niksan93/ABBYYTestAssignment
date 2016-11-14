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
    public class UnitTestMainPage<TIWebDriver> : BaseTest<TIWebDriver> where TIWebDriver : IWebDriver, new()
    {
        // BaseTest variables for browsers.
        BaseTest<ChromeDriver> baseTestChrome;
        BaseTest<FirefoxDriver> baseTestFirefox;
        BaseTest<InternetExplorerDriver> baseTestInternetExplorer;
        BaseTest<IWebDriver> baseTest;
        // Web page variables.
        MainPage pageChrome;
        MainPage pageFirefox;
        MainPage pageInternetExplorer;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            baseTest = new BaseTest<ChromeDriver>(MainPage.Url);
            //baseTestChrome = new BaseTest<ChromeDriver>(MainPage.Url);
            //baseTestFirefox = new BaseTest<FirefoxDriver>(MainPage.Url);
            //baseTestInternetExplorer = new BaseTest<InternetExplorerDriver>(MainPage.Url);
            pageChrome = new MainPage(baseTestChrome.WebDriver);
            //pageFirefox = new MainPage(baseTestFirefox.WebDriver);
            //pageInternetExplorer = new MainPage(baseTestInternetExplorer.WebDriver);
        }

        /// <summary>
        /// A method to be performed after all tests in this class are completed regardless of the outcome.
        /// Quits all the IWebDrivers.
        /// </summary>
        [OneTimeTearDown]
        public void DisposeAll()
        {
            baseTestChrome.Dispose();
            //baseTestFirefox.Dispose();
            //baseTestInternetExplorer.Dispose();
        }

        [Test]
        public void TestContactInfoChrome()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckPhoneText(baseTestChrome.WebDriver);
            }, PageInfo.ContactInfo);
        }

        [Test]
        public void TestLangSwitcherExistence()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherExistence(baseTestChrome.WebDriver);
            }, PageInfo.LangSwitcherExistence);
        }

        [Test]
        public void TestLangSwitcherElements()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherElements(baseTestChrome.WebDriver);
            }, PageInfo.LangSwitcherElements);
        }

        /// <summary>
        /// Test that correct images appear on pressing certain buttons in the left menu.
        /// Tests Chrome.
        /// </summary>
        [Test]
        public void TestImageDisplayedChrome()
        {
            try
            {
                Assert.IsTrue(pageChrome.checkImage());
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.MainPage, baseTestChrome.WebDriver);
                baseTestChrome.WebDriver.Quit();
                throw new AssertionException("Image was not correctly displayed");
            }
        }

        /// <summary>
        /// Test that correct images appear on pressing certain buttons in the left menu.
        /// Tests Firefox.
        /// </summary>
        
        public void TestImageDisplayedFirefox()
        {
            pageFirefox.checkImage();
        }

        /// <summary>
        /// Test that correct images appear on pressing certain buttons in the left menu.
        /// Tests InternetExplorer.
        /// </summary>
        
        public void TestImageDisplayedIE()
        {
            pageInternetExplorer.checkImage();
        }

        /// <summary>
        /// Test that positions of current image and the rest on the base image are different.
        /// I.e. Test that correct images appear on pressing certain buttons in the left menu.
        /// </summary>
        
        public void TestImagePosition()
        {
            try
            {
                Assert.IsTrue(pageChrome.checkImagePosition());
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.MainPage, baseTestChrome.WebDriver);
                baseTestChrome.WebDriver.Quit();
                throw new AssertionException("Image was not correctly displayed.");
            }
            pageFirefox.checkImagePosition();
            pageInternetExplorer.checkImagePosition();
        }
    }
}
