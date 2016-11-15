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
    public class UnitTestIterpOfferPage<TIWebDriver> : BaseTest<TIWebDriver> where TIWebDriver : IWebDriver, new()
    {
        // Web page variables.
        InterpOfferPage Page;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            BaseTest<TIWebDriver>.Initialize(InterpOfferPage.Url);
            Page = new InterpOfferPage(BaseTest<TIWebDriver>.WebDriver);
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
        /// Test 'Activity type' dropbox for emptines.
        /// </summary>
        [Test]
        public void TestActivityBoxEmpty()
        {
            try
            {
                Assert.IsTrue(Page.CheckActivityBox(ActivityBoxCheck.IsEmpty));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.InterpOfferPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "'Activity type' dropbox is empty.";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Test if 'Activity type' dropbox is enabled.
        /// </summary>
        [Test]
        public void TestActivityBoxEnabled()
        {
            try
            {
                Assert.IsTrue(Page.CheckActivityBox(ActivityBoxCheck.IsEnabled));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.InterpOfferPage, BaseTest<TIWebDriver>.WebDriver);
                string exMsg = "'Activity type' dropbox is disabled. Not possible to choose activity..";
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Child method for testing contact info presence on page.
        /// </summary>
        [Test]
        public void TestContactInfoInterpPage()
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
        public void TestLangSwitcherExistenceInterpPage()
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
        public void TestLangSwitcherElementsInterpPage()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherElements(BaseTest<TIWebDriver>.WebDriver);
            }, PageInfo.LangSwitcherElements);
        }
    }
}
