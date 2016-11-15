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
    public class UnitTestCalcPage<TIWebDriver> : BaseTest<TIWebDriver> where TIWebDriver : IWebDriver, new()
    {
        // Web page variables.
        CalculatorPage Page;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            BaseTest<TIWebDriver>.Initialize(CalculatorPage.Url);
            Page = new CalculatorPage(BaseTest<TIWebDriver>.WebDriver);
        }

        /// <summary>
        /// A method to be performed after all tests in this class are completed regardless of the outcome.
        /// Quits all the IWebDrivers.
        /// </summary>
        [OneTimeTearDown]
        public void DisposeAll()
        {
            BaseTest<TIWebDriver>.Dispose();
        }

        /// <summary>
        /// Test 'From language' dropbox for emptiness
        /// </summary>
        [Test]
        public void TestfromLangDropBox()
        {
            try
            {
                Assert.IsTrue(Page.CheckLangDropboxEmpty(DropboxType.From));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.CalcPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "From language' dropbox is empty";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Test 'From language' dropbox to have 'Русский' as an option.
        /// </summary>
        [Test]
        public void TestFromLangOptions()
        {
            try
            {
                Assert.IsTrue(Page.CheckLangOptions(DropboxType.From));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.CalcPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "'Русский' is not found in the 'From language' dropbox";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Test 'To language' dropbox for emptiness.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public void TestToLangDropBox()
        {
            try
            {
                Page.CheckLangOptions(DropboxType.From);
                // Wait until previous action is correcty completed in browser.
                BaseTest<TIWebDriver>.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                Assert.IsTrue(Page.CheckLangDropboxEmpty(DropboxType.To));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.CalcPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "To language' dropbox is empty";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Test 'To language' dropbox for 'Английский' as an option.
        /// Actions to choose option 'Русский' in the 'From language' dropbox
        /// are perfomed, since 'To language' is empty by default and requires 'From language' chosen.
        /// </summary>
        [Test]
        public void TestToLangOptions()
        {
            try
            {
                Page.CheckLangOptions(DropboxType.From);
                // Wait until previous action is correcty completed in browser.
                BaseTest<TIWebDriver>.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                Assert.IsTrue(Page.CheckLangOptions(DropboxType.To));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.CalcPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "'Английский' is not found in the 'To language' dropbox";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Child method for testing contact info presence on page.
        /// </summary>
        [Test]
        public void TestContactInfoCalcPage()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckPhoneText(BaseTest<TIWebDriver>.WebDriver);
            }, PageInfo.ContactInfo);
        }

        /// <summary>
        /// Chold method for testing language switcher existence on page.
        /// </summary>
        [Test]
        public void TestLangSwitcherExistenceCalcPage()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherExistence(BaseTest<TIWebDriver>.WebDriver);
            }, PageInfo.LangSwitcherExistence);
        }

        /// <summary>
        /// Child method for testing language switcher for right elements.
        /// </summary>
        [Test]
        public void TestLangSwitcherElementsCalcPage()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherElements(BaseTest<TIWebDriver>.WebDriver);
            }, PageInfo.LangSwitcherElements);
        }
    }
}
