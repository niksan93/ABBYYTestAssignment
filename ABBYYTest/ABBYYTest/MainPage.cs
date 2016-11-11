using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ABBYYTest
{
    public class MainPage
    {
        // IWebDriver
        private static IWebDriver driver;
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
        public IReadOnlyCollection<IWebElement> getLeftMenu()
        {
            return driver.FindElements(menuLocator);
        }
        /// <summary>
        /// IReadOnlyCollection of WebElements containing images.
        /// </summary>
        /// <returns>IReadOnlyCollection of WebElements containing images</returns>
        public IReadOnlyCollection<IWebElement> getImagesInfo()
        {
            return driver.FindElements(imagesInfoLocator);
        }
        /// <summary>
        /// Get the number of elements in the left menu.
        /// </summary>
        /// <returns>The number of elements in the left menu</returns>
        public int getMenuCount()
        {
            return driver.FindElements(menuLocator).Count;
        }
        /// <summary>
        /// Get background Y positions for images from CSS.
        /// </summary>
        /// <returns>List of int values of background Y positions for images</returns>
        public List<int> getBackGroundPosYForImages()
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
                Console.WriteLine("Exception: {0}", ex.ToString());
                return null;
            }
            return backGroundPosList;
        }
    }
}
