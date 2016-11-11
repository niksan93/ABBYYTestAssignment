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
        private static MainPage mainPage;
        private static IWebDriver driver;
        /// <summary>
        /// Chrome test.
        /// </summary>
        [Test]
        public void chromeMainPageTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            testImagesOnMain();
            driver.Quit();
        }
        /// <summary>
        /// Firefox test.
        /// </summary>
        [Test]
        public void firefoxMainPageTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            testImagesOnMain();
            driver.Quit();
        }
        /// <summary>
        /// Internet explorer test.
        /// </summary>
        [Test]
        public void ieMainPageTest()
        {
            driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            testImagesOnMain();
            driver.Quit();
        }

        /// <summary>
        /// Test if right images appear from right buttons.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        static void testImagesOnMain()
        {
            driver.Navigate().GoToUrl(MainPage.url);
            mainPage = new MainPage(driver);
            AdditionalFunc.checkPhoneText(driver);
            AdditionalFunc.checkLangSwitcherExistence(driver);
            AdditionalFunc.checkLangSwitcherElements(driver);
            int menuCount = mainPage.getMenuCount();
            for (int i = 0; i < menuCount; i++)
            {
                checkImage(i, driver);
            }
        }

        /// <summary>
        /// Check current image if it is displayed and ( if its position differes from the rest ).
        /// </summary>
        /// <param name="number">Number of current image</param>
        /// <param name="driver">IWebDriver</param>
        static void checkImage(int number, IWebDriver driver)
        {
            mainPage.getLeftMenu().ElementAt(number).Click();
            Thread.Sleep(1000);
            try
            {
                Assert.IsTrue(mainPage.getImagesInfo().ElementAt(number).Displayed);
                if (mainPage.getBackGroundPosYForImages() != null)
                {
                    checkImagePosition(number);
                }
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenshotMainPage(number, driver);
                driver.Quit();
                throw new AssertionException("Image number" + (number - 1) +  "was not correctly displayed");
            }
        }

        /// <summary>
        /// Check if positions of current image and the rest are different.
        /// </summary>
        /// <param name="number">Number of current image</param>
        /// <param name="driver">IWebDriver</param>
        static void checkImagePosition(int number)
        {
            int elementsCount = mainPage.getBackGroundPosYForImages().Count;
            int counter = 0;
            try
            {
                for (counter = 0; counter < elementsCount && counter != number; counter++)
                {
                    List<int> backGrPositions = mainPage.getBackGroundPosYForImages();
                    Assert.IsTrue(backGrPositions.ElementAt(number) != backGrPositions.ElementAt(counter));
                }
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenshotMainPage(number, driver);
                driver.Quit();
                throw new AssertionException("Images positions at" + (number - 1) + " and " + (counter - 1) + " are equal.");
            }
        }
    }
}
