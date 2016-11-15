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
        // Web page variables.
        MainPage Page;

        /// <summary>
        /// A one time setup method for tests in this class.
        /// Initialise BaseTest and Web page variables.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpMainPage()
        {
            BaseTest<TIWebDriver>.Initialize(MainPage.Url);
            Page = new MainPage(BaseTest<TIWebDriver>.WebDriver);
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
        /// Child method for testing contact info presence on page.
        /// </summary>
        [Test]
        public void TestContactInfoMainPage()
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
        public void TestLangSwitcherExistenceMainPage()
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
        public void TestLangSwitcherElementsMainPage()
        {
            BaseTestInfo(() =>
            {
                BasePage.CheckLangSwitcherElements(BaseTest<TIWebDriver>.WebDriver);
            }, PageInfo.LangSwitcherElements);
        }

        /// <summary>
        /// Test that correct images appear on pressing certain buttons in the left menu.
        /// </summary>
        [Test]
        public void TestImageDisplayed()
        {
            try
            {
                Assert.IsTrue(Page.checkImage(BaseTest<TIWebDriver>.WebDriver));
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.MainPage, BaseTest<TIWebDriver>.WebDriver);
                throw new AssertionException("Image was not correctly displayed");
            }
        }

        /// <summary>
        /// Test that positions of current image and the rest on the base image are different.
        /// I.e. Test that correct images appear on pressing certain buttons in the left menu.
        /// </summary>
        [Test]
        public void TestImagePosition()
        {
            try
            {
                Assert.IsTrue(Page.checkImagePosition());
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.MainPage, BaseTest<TIWebDriver>.WebDriver);
                throw new AssertionException("Image was not correctly displayed.");
            }
        }
    }
}
