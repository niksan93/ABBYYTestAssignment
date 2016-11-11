using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ABBYYTest
{
    public class InterpOfferPage
    {
        // IWebDriver
        private static IWebDriver driver;
        // Page url.
        public static string url = "http://abbyy-ls.ru/interpreting_offer";

        By activityTypeLocator = By.ClassName("form-select");

        public InterpOfferPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        /// <summary>
        /// Dropbox 'activity type'.
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement getActivityBoxElement()
        {
            return driver.FindElement(activityTypeLocator);
        }
        /// <summary>
        /// Options of 'activity type' dropbox.
        /// </summary>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        public IList<IWebElement> getActivityBoxOptions()
        {
            return new SelectElement(getActivityBoxElement()).Options;
        }
    }
}
