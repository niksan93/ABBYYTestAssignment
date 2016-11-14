using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using NUnit.Framework;


namespace ABBYYTest
{
    public enum DropboxType { From, To }
    public class CalculatorPage
    {
        // IWebDriver
        IWebDriver driver;
        // Page url.
        readonly static string url = "http://abbyy-ls.ru/doc-calculator";
        // By Locators.
        readonly By fromLangLocator = By.Name("from-lang");
        readonly By toLangLocator = By.Name("to-lang");

        public CalculatorPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        static CalculatorPage()
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
        /// Dropbox 'from/to language'.
        /// </summary>
        /// <param name="from">true: from language
        /// false: to language</param>
        /// <returns>IWebElement of dropbox</returns>
        IWebElement GetLangElement(DropboxType type)
        {
            if (type == DropboxType.From)
                return driver.FindElement(fromLangLocator);
            else
                return driver.FindElement(toLangLocator);
        }
        /// <summary>
        /// Options of 'from/to language' dropbox.
        /// </summary>
        /// <param name="from">true: from language
        /// false: to language</param>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        IList<IWebElement> GetLangOptions(DropboxType type)
        {
            return new SelectElement(GetLangElement(type)).Options;
        }

        /// <summary>
        /// Check if 'from/to lang' dropbox is not empty.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        /// <param name="from">true: 'from language'
        /// false: 'to language'</param>
        public void CheckLangDropboxEmpty(DropboxType type)
        {
            try
            {
                IWebElement dropBox = GetLangElement(type);
                IList<IWebElement> langOptions = GetLangOptions(type);
                Assert.IsTrue(langOptions.Count >= 1);
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.CalcPage, driver);
                driver.Quit();
                string exDropbox = type == DropboxType.From ? "From" : "To";
                string exMsg = string.Format("{0} language' dropbox is empty", exDropbox);
                throw new AssertionException(exMsg);
            }
        }

        /// <summary>
        /// Check if 'from/to language' droppbox contains options 'Русский'/'Английский' respectively.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        /// <param name="from">true: 'from language'
        /// false: 'to language'</param>
        public void CheckLangOptions(DropboxType type)
        {
            IWebElement dropBox = GetLangElement(type);
            IList<IWebElement> langOptions = GetLangOptions(type);
            int optionsCount = langOptions.Count;
            dropBox.Click();
            for (int dropBoxOption = 0; dropBoxOption < optionsCount; dropBoxOption++)
            {
                IWebElement option = langOptions[dropBoxOption];
                if (option.GetAttribute("text").Equals("Русский") && type == DropboxType.From ||
                    option.GetAttribute("text").Equals("Английский") && type == DropboxType.To)
                {
                    // It is a known issue, that this might occasionally not work with Firefox
                    SelectElement selectEl = new SelectElement(dropBox);
                    selectEl.SelectByIndex(dropBoxOption);
                    return;
                }
            }
            BasePage.TakeScreenshot(ScreenShotType.CalcPage, driver);
            driver.Quit();
            string exLang = type == DropboxType.From ? "'Русский'" : "'Английский'";
            string exDropbox = type == DropboxType.From ? "from" : "to";
            string exMsg = string.Format("{0} is not found in the '{1} language' dropbox", exLang, exDropbox);
            throw new AssertionException(exMsg);
        }
    }
}
