using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace ABBYYTest
{
    public class MainPage
    {
        // IWebDriver
        private IWebDriver driver;
        // Page url.
        public static string url = "http://abbyy-ls.ru/";

        By menuLocator = By.ClassName("control-slider");
        By imagesInfoLocator = By.ClassName("frontslider2-rightcol-img");

        public MainPage(IWebDriver wdriver)
        {
            driver = wdriver;        
        }
        /// <summary>
        /// IReadOnlyCollection of buttons to the left of the screen.
        /// </summary>
        /// <returns>IReadOnlyCollection of buttons to the left of the screen</returns>
        private IReadOnlyCollection<IWebElement> getLeftMenu()
        {
            return driver.FindElements(menuLocator);
        }
        /// <summary>
        /// IReadOnlyCollection of WebElements containing images.
        /// </summary>
        /// <returns>IReadOnlyCollection of WebElements containing images</returns>
        private IReadOnlyCollection<IWebElement> getImagesInfo()
        {
            return driver.FindElements(imagesInfoLocator);
        }
        /// <summary>
        /// Get the number of elements in the left menu.
        /// </summary>
        /// <returns>The number of elements in the left menu</returns>
        private int getMenuCount()
        {
            return driver.FindElements(menuLocator).Count;
        }
        /// <summary>
        /// Get background Y positions for images from CSS.
        /// </summary>
        /// <returns>List of int values of background Y positions for images</returns>
        private List<int> getBackGroundPosYForImages()
        {
            IReadOnlyCollection<IWebElement> imagesInfo = driver.FindElements(imagesInfoLocator);
            int imgNumber = imagesInfo.Count;
            List<int> backGroundPosList = new List<int>();
            try
            {
                for (int i = 0; i < imgNumber; i++)
                {
                    string backgroundPosY = imagesInfo.ElementAt(i).GetCssValue("background-position-y");
                    backGroundPosList.Add(Int32.Parse(backgroundPosY.TrimEnd('p', 'x')));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error obtaining background position y of slide image from CSS: {0}", ex.ToString());
                return null;
            }
            return backGroundPosList;
        }

        /// <summary>
        /// Check current image if it is displayed and ( if its position differes from the rest ).
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public void checkImage()
        {
            int menuCount = getMenuCount();
            for (int number = 0; number < menuCount; number++)
            {
                getLeftMenu().ElementAt(number).Click();
                Thread.Sleep(1000);
                try
                {
                    Assert.IsTrue(getImagesInfo().ElementAt(number).Displayed);
                }
                catch (AssertionException)
                {
                    BasePage.takeScreenshot(ScreenShotType.MainPage, number, driver);
                    driver.Quit();
                    throw new AssertionException("Image number" + (number - 1) + "was not correctly displayed");
                }
            }
        }

        /// <summary>
        /// Check if positions of current image and the rest are different.
        /// </summary>
        /// <param name="number">Number of current image</param>
        /// <param name="driver">IWebDriver</param>
        public void checkImagePosition()
        {
            int menuCount = getMenuCount();
            for (int number = 0; number < menuCount; number++)
            {
                int elementsCount = getBackGroundPosYForImages().Count;
                int counter = 0;
                try
                {
                    for (counter = 0; counter < elementsCount && counter != number; counter++)
                    {
                        List<int> backGrPositions = getBackGroundPosYForImages();
                        Assert.IsTrue(backGrPositions.ElementAt(number) != backGrPositions.ElementAt(counter));
                    }
                }
                catch (AssertionException)
                {
                    BasePage.takeScreenshot(ScreenShotType.MainPage, number, driver);
                    driver.Quit();
                    throw new AssertionException("Images positions at" + (number - 1) + " and " + (counter - 1) + " are equal.");
                }
            }
        }
    }
}
