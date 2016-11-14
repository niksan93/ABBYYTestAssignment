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
    public enum DroboxType { From, To }
    public class CalculatorPage
    {
        // IWebDriver
        private IWebDriver driver;
        // Page url.
        public static string url = "http://abbyy-ls.ru/doc-calculator";
        // By Locators.
        By fromLangLocator = By.Name("from-lang");
        By toLangLocator = By.Name("to-lang");

        public CalculatorPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        /// <summary>
        /// Dropbox 'from/to language'.
        /// </summary>
        /// <param name="from">true: from language
        /// false: to language</param>
        /// <returns>IWebElement of dropbox</returns>
        IWebElement getLangElement(DroboxType type)
        {
            if (type == DroboxType.From)
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
        IList<IWebElement> getLangOptions(DroboxType type)
        {
            if (type == DroboxType.From)
                return new SelectElement(getLangElement(type)).Options;
            else
                return new SelectElement(getLangElement(type)).Options;
        }

        /// <summary>
        /// Check if 'from/to lang' dropbox is not empty.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        /// <param name="from">true: 'from language'
        /// false: 'to language'</param>
        public void checkLangDropboxEmpty(DroboxType type)
        {
            try
            {
                IWebElement dropBox = getLangElement(type);
                IList<IWebElement> langOptions = getLangOptions(type);
                Assert.IsTrue(langOptions.Count >= 1);
            }
            catch (AssertionException)
            {
                BasePage.takeScreenshot(ScreenShotType.CalcPage, 0, driver);
                driver.Quit();
                if (type == DroboxType.From)
                    throw new AssertionException("'from language' dropbox is empty");
                else
                    throw new AssertionException("'to language' dropbox is empty");
            }
        }

        /// <summary>
        /// Check if 'from/to language' droppbox contains options 'Русский'/'Английский' respectively.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        /// <param name="from">true: 'from language'
        /// false: 'to language'</param>
        public void checkLangOptions(DroboxType type)
        {
            IWebElement dropBox = getLangElement(type);
            IList<IWebElement> langOptions = getLangOptions(type);
            int optionsCount = langOptions.Count;
            dropBox.Click();
            for (int dropBoxOption = 0; dropBoxOption < optionsCount; dropBoxOption++)
            {
                IWebElement option = langOptions[dropBoxOption];
                if (option.GetAttribute("text").Equals("Русский") && type == DroboxType.From)
                {
                    /*if (((RemoteWebDriver)driver).Capabilities.BrowserName.Equals("firefox"))
                    {
                        // It is a known issue that selecting options in dropdown boxes does't quite work in geckodriver.
                        // Neither .SelectByText(), nor .SelectByIndex() or simple .Click() seems to work correctly in FireFox.
                        // Since other methods don't seem to work for geckodriver, keyboard is used in this case for choosing dropbox options.
                        for (int j = 0; j < dropBoxOption; j++)
                        {
                            option.SendKeys(Keys.ArrowDown);
                        }
                        option.SendKeys(Keys.Enter);
                    }
                    else
                    {*/
                        SelectElement selectEl = new SelectElement(dropBox);
                        selectEl.SelectByIndex(dropBoxOption);
                    //}
                    return;
                }
                else
                    if (option.GetAttribute("text").Equals("Английский") && type == DroboxType.To)
                    {
                        SelectElement selectEl = new SelectElement(dropBox);
                        selectEl.SelectByIndex(dropBoxOption);
                        return;
                    }
            }
            BasePage.takeScreenshot(ScreenShotType.CalcPage, 0, driver);
            driver.Quit();
            if (type == DroboxType.From)
                throw new AssertionException("'Русский' is not found in the 'from language' dropbox");
            else
                throw new AssertionException("'Английский' is not found in the 'to language' dropbox");
        }
    }
}
