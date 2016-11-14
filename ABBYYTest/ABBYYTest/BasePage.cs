using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace ABBYYTest
{
    public enum ScreenShotType { MainPage, CalcPage, InterpOfferPage, ContactInfo, LanguageChange }
    public class BasePage
    {
        // Image path to images folder in the solution directory.
        readonly static string imagePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\images\\";
        // ABBYY contact phone.
        readonly static string contactPhone = "+7 (495) 374-56-08";
        // Language url list in the switcher.
        readonly static string[] langList = { "abbyy-ls.com", "abbyy-ls.de", "abbyy-ls.ua", "abbyy-ls.kz" };
        // By Locators.
        readonly static By phoneNumberLocator = By.ClassName("call_phone_1");
        readonly static By langSwitcherLocator = By.ClassName("lang-switcher");
        readonly static By langSwitcherItemLocator = By.ClassName("lang-switcher__item");

        /// <summary>
        /// Check if directory "images" already exists, else create it.
        /// </summary>
        static void CheckImageDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo imagesDir = Directory.CreateDirectory(path);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Failed to access images directory");
            }
        }

        /// <summary>
        /// Take a screenshot of a page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="scrType">type of screenshot: which name to set to image</param>
        /// <param name="driver">IWebDriver</param>
        public static void TakeScreenshot(ScreenShotType scrType, IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            CheckImageDirectory(imagePath);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            switch (scrType)
            {
                case ScreenShotType.MainPage:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "WrongImageOnMainPage.jpg", ImageFormat.Jpeg);
                    break;
                case ScreenShotType.CalcPage:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnCalculatorPage.jpg", ImageFormat.Jpeg);
                    break;
                case ScreenShotType.InterpOfferPage:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnInterpretOfferPage.jpg", ImageFormat.Jpeg);
                    break;
                case ScreenShotType.ContactInfo:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorWithContactInfo.jpg", ImageFormat.Jpeg);
                    break;
                case ScreenShotType.LanguageChange:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorWithLanguageChange.jpg", ImageFormat.Jpeg);
                    break;
            }
        }

        /// <summary>
        /// Check text of phone element.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static bool CheckPhoneText(IWebDriver driver)
        {
            return driver.FindElement(phoneNumberLocator).Text.Equals(contactPhone);
        }
        /// <summary>
        /// Check existence language switcher element.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static bool CheckLangSwitcherExistence(IWebDriver driver)
        {
            return driver.FindElements(langSwitcherLocator).Count != 0;
        }
        /// <summary>
        /// Check if there are exactly 4 languagues in language dropbox:
        /// русский, немецкий, украинский, английский.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static bool CheckLangSwitcherElements(IWebDriver driver)
        {
            driver.FindElement(langSwitcherLocator).Click();
            IList<IWebElement> list = driver.FindElements(langSwitcherItemLocator);
            CheckLangListCount(list, driver);
            bool result = false;
            for (int i = 0; i < 4; i++)
            {
                result = result || langList.Any(list[i].GetAttribute("href").Contains);
            }
            return result;
        }
        /// <summary>
        /// Check if the amount of languages in dropbox is 4.
        /// </summary>
        /// <param name="list">IList of language elements</param>
        /// <param name="driver">IWebDriver</param>
        public static void CheckLangListCount(IList<IWebElement> list, IWebDriver driver)
        {
            if (list.Count != 4)
            {
                driver.FindElement(langSwitcherLocator).Click();
                TakeScreenshot(ScreenShotType.LanguageChange, driver);
                driver.Quit();
                throw new Exception("Unexpected amount of languages");
            }
        }
    }
}
