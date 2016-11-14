using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace ABBYYTest
{
    public enum ActivityBoxCheck { IsEmpty, IsEnabled }
    public class InterpOfferPage
    {
        // IWebDriver
        static IWebDriver driver;
        // Page url.
        readonly static string url = "http://abbyy-ls.ru/interpreting_offer";
        // By Locators.
        readonly By activityTypeLocator = By.ClassName("form-select");

        public InterpOfferPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        static InterpOfferPage()
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
        /// Dropbox 'activity type'.
        /// </summary>
        /// <returns>IWebElement</returns>
        IWebElement GetActivityBoxElement()
        {
            return driver.FindElement(activityTypeLocator);
        }
        /// <summary>
        /// Options of 'activity type' dropbox.
        /// </summary>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        IList<IWebElement> GetActivityBoxOptions()
        {
            return new SelectElement(GetActivityBoxElement()).Options;
        }

        /// <summary>
        /// Check 'activity type' dropbox emptiness \ 'enabled' status.
        /// </summary>
        /// <param name="type">ActivityBoxCheck.IsEmpty = check for emptiness
        /// ActivityBoxCheck.IsEnabled = check if dropbox is enabled</param>
        public void CheckActivityBox(ActivityBoxCheck type)
        {
            try
            {
                IWebElement activityBox = GetActivityBoxElement();
                activityBox.Click();
                IList<IWebElement> activityOptions = GetActivityBoxOptions();
                if (type == ActivityBoxCheck.IsEmpty)
                    Assert.IsTrue(activityOptions.Count >= 1);
                else
                    Assert.IsTrue(activityBox.Enabled);
            }
            catch (AssertionException)
            {
                BasePage.TakeScreenshot(ScreenShotType.InterpOfferPage, driver);
                driver.Quit();
                string exDropbox = type == ActivityBoxCheck.IsEmpty ? "empty." : "disabled. Not possible to choose activity.";
                string exMsg = string.Format("'Activity type' dropbox is {0}", exDropbox);
                throw new AssertionException(exMsg);
            }
        }
    }
}
