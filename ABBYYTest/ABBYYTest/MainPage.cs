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
        IWebDriver driver;
        // Page url.
        readonly static string url = "http://abbyy-ls.ru/";
        // By Locators.
        readonly By menuLocator = By.ClassName("control-slider");
        readonly By imagesInfoLocator = By.ClassName("frontslider2-rightcol-img");

        public MainPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        /// <summary>
        /// Static constructor to initialize static property.
        /// </summary>
        static MainPage()
        {
            Url = url;
        }
        /// <summary>
        /// Url property
        /// </summary>
        public static string Url
        {
            get;
            set;
        }
        /// <summary>
        /// IReadOnlyCollection of buttons to the left of the screen.
        /// </summary>
        /// <returns>IReadOnlyCollection of buttons to the left of the screen</returns>
        IReadOnlyCollection<IWebElement> GetLeftMenu()
        {
            return driver.FindElements(menuLocator);
        }
        /// <summary>
        /// IReadOnlyCollection of WebElements containing images.
        /// </summary>
        /// <returns>IReadOnlyCollection of WebElements containing images</returns>
        IReadOnlyCollection<IWebElement> GetImagesInfo()
        {
            return driver.FindElements(imagesInfoLocator);
        }
        /// <summary>
        /// Get the number of elements in the left menu.
        /// </summary>
        /// <returns>The number of elements in the left menu</returns>
        int GetMenuCount()
        {
            return driver.FindElements(menuLocator).Count;
        }
        /// <summary>
        /// Get background Y positions for images from CSS.
        /// </summary>
        /// <returns>List of int values of background Y positions for images</returns>
        List<int> GetBackGroundPosYForImages()
        {
            IReadOnlyCollection<IWebElement> imagesInfo = driver.FindElements(imagesInfoLocator);
            int imgNumber = imagesInfo.Count;
            List<int> backGroundPosList = new List<int>();
            try
            {
                for (int i = 0; i < imgNumber; i++)
                {
                    string backgroundPosY = imagesInfo.ElementAt(i).GetCssValue("background-position-y");
                    backgroundPosY = backgroundPosY.TrimEnd('p', 'x');
                    int position = 0;
                    Int32.TryParse(backgroundPosY, out position);
                    backGroundPosList.Add(position);
                }
            }
            catch (Exception)
            {
                driver.Quit();
                throw new Exception("Error obtaining background position y of slide image from CSS.");
            }
            return backGroundPosList;
        }

        /// <summary>
        /// Check current image if it is displayed and ( if its position differes from the rest ).
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public bool checkImage()
        {
            int menuCount = GetMenuCount();
            for (int number = 0; number < menuCount; number++)
            {
                GetLeftMenu().ElementAt(number).Click();
                Thread.Sleep(1000);
                if (!GetImagesInfo().ElementAt(number).Displayed)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check if positions of current image and the rest in CSS are different.
        /// </summary>
        public bool checkImagePosition()
        {
            int menuCount = GetMenuCount();
            List<int> BackGrPositions = GetBackGroundPosYForImages();
            bool isUnique = BackGrPositions.Distinct().Count() == menuCount;
            return isUnique;
        }
    }
}
