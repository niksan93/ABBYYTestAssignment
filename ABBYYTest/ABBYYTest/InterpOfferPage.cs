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
        private static IWebDriver driver;
        // Page url.
        public static string url = "http://abbyy-ls.ru/interpreting_offer";
        // By Locators.
        By activityTypeLocator = By.ClassName("form-select");

        public InterpOfferPage(IWebDriver wdriver)
        {
            driver = wdriver;
        }

        /// <summary>
        /// Dropbox 'activity type'.
        /// </summary>
        /// <returns>IWebElement</returns>
        private IWebElement getActivityBoxElement()
        {
            return driver.FindElement(activityTypeLocator);
        }
        /// <summary>
        /// Options of 'activity type' dropbox.
        /// </summary>
        /// <returns>Ilist of IWebElements, options of dropbox</returns>
        private IList<IWebElement> getActivityBoxOptions()
        {
            return new SelectElement(getActivityBoxElement()).Options;
        }

        /// <summary>
        /// Check 'activity type' dropbox emptiness \ 'enabled' status.
        /// </summary>
        /// <param name="type">ActivityBoxCheck.IsEmpty = check for emptiness
        /// ActivityBoxCheck.IsEnabled = check if dropbox is enabled</param>
        public void checkActivityBox(ActivityBoxCheck type)
        {
            try
            {
                IWebElement activityBox = getActivityBoxElement();
                activityBox.Click();
                IList<IWebElement> activityOptions = getActivityBoxOptions();
                if (type == ActivityBoxCheck.IsEmpty)
                    Assert.IsTrue(activityOptions.Count >= 1);
                else
                    Assert.IsTrue(activityBox.Enabled);
            }
            catch (AssertionException)
            {
                BasePage.takeScreenshot(ScreenShotType.InterpOfferPage, 0, driver);
                driver.Quit();
                if (type == ActivityBoxCheck.IsEmpty)
                    throw new AssertionException("'Activity type' dropbox is empty");
                else
                    throw new AssertionException("'Activity type' dropbox is disabled. Not possible to choose activity.");
            }
        }
    }
}
