using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ABBYYTest
{
    public class CalculatorPage
    {
        // IWebDriver
        private static IWebDriver driver;
        // Page url.
        public static string url = "http://abbyy-ls.ru/doc-calculator";

        By fromLangLocator = By.Name("from-lang");
        By toLangLocator = By.Name("to-lang");

        public CalculatorPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        /// <summary>
        /// Dropbox 'from language'.
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement getFromLangElement()
        {
            return driver.FindElement(fromLangLocator);
        }
        /// <summary>
        /// Options of 'from language' dropbox.
        /// </summary>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        public IList<IWebElement> getFromLangOptions()
        {
            return new SelectElement(getFromLangElement()).Options;
        }
        /// <summary>
        /// Dropbox 'to language'.
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement getToLangElement()
        {
            return driver.FindElement(toLangLocator);
        }
        /// <summary>
        /// Options of 'to language' dropbox.
        /// </summary>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        public IList<IWebElement> getToLangOptions()
        {
            return new SelectElement(getToLangElement()).Options;
        }
    }
}
