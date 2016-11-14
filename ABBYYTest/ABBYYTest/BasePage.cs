using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        // IWebDriver
       // private IWebDriver driver;
        // Image path to images folder in the solution directory.
        static string imagePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\images\\";
        // ABBYY contact phone.
        public static string contactPhone = "+7 (495) 374-56-08";
        // Language url list in the switcher.
        public static string[] langList = { "abbyy-ls.com", "abbyy-ls.de", "abbyy-ls.ua", "abbyy-ls.kz" };
        // By Locators.
        static By phoneNumberLocator = By.ClassName("call_phone_1");
        static By langSwitcherLocator = By.ClassName("lang-switcher");
        static By langSwitcherItemLocator = By.ClassName("lang-switcher__item");

        public BasePage(IWebDriver wdriver)
        {
          ///  driver = wdriver;        
        }

        /// <summary>
        /// Check if directory "images" already exists, else create it.
        /// </summary>
        static void checkImageDirectory(string path, IWebDriver driver)
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
                driver.Quit();
                throw new IOException("Failed to access images directory");
            }
        }

        /// <summary>
        /// Take a screenshot of a page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="scrType">type of screenshot: which name to set to image</param>
        /// <param name="imageNumber">Number of current image (ScreenShotType.MainPage)</param>
        /// <param name="driver">IWebDriver</param>
        public static void takeScreenshot(ScreenShotType scrType, int imageNumber, IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            checkImageDirectory(imagePath, driver);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            switch (scrType)
            {
                case ScreenShotType.MainPage:
                    scrFile.SaveAsFile(imagePath + ++imageNumber + "Menu" + capabilities.BrowserName + "WrongImageOnMainPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ScreenShotType.CalcPage:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnCalculatorPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ScreenShotType.InterpOfferPage:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnInterpretOfferPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ScreenShotType.ContactInfo:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorWithContactInfo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ScreenShotType.LanguageChange:
                    scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorWithLanguageChange.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
            }
        }

        /// <summary>
        /// Check text of phone element.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void checkPhoneText(IWebDriver driver)
        {
            try
            {
                Assert.IsTrue(driver.FindElement(phoneNumberLocator).Text.Equals(contactPhone));
            }
            catch (AssertionException)
            {
                takeScreenshot(ScreenShotType.ContactInfo, 0, driver);
                driver.Quit();
                throw new AssertionException("Phone number is not correct on page" + driver.Url);
            }
        }
        /// <summary>
        /// Check existence language switcher element.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void checkLangSwitcherExistence(IWebDriver driver)
        {
            try
            {
                Assert.IsTrue(driver.FindElements(langSwitcherLocator).Count != 0);
            }
            catch (AssertionException)
            {
                takeScreenshot(ScreenShotType.LanguageChange, 0, driver);
                driver.Quit();
                throw new AssertionException("There is no language switcher element on page" + driver.Url);
            }
        }
        /// <summary>
        /// Check if there are exactly 4 languagues in language dropbox:
        /// русский, немецкий, украинский, английский.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void checkLangSwitcherElements(IWebDriver driver)
        {
            try
            {
                IList<IWebElement> list = driver.FindElements(langSwitcherItemLocator);
                checkLangListCount(list, driver);
                Assert.IsTrue((checkStringContains(list[0].GetAttribute("href"), langList) &&
                    checkStringContains(list[1].GetAttribute("href"), langList) &&
                    checkStringContains(list[2].GetAttribute("href"), langList) &&
                    checkStringContains(list[2].GetAttribute("href"), langList)));
            }
            catch (AssertionException)
            {
                driver.FindElement(langSwitcherLocator).Click();
                takeScreenshot(ScreenShotType.LanguageChange, 0, driver);
                driver.Quit();
                throw new Exception("Unexpected languages in language drop box");
            }
        }
        /// <summary>
        /// Check if the amount of languages in dropbox is 4.
        /// </summary>
        /// <param name="list">IList of language elements</param>
        /// <param name="driver">IWebDriver</param>
        public static void checkLangListCount(IList<IWebElement> list, IWebDriver driver)
        {
            if (list.Count != 4)
            {
                driver.FindElement(langSwitcherLocator).Click();
                takeScreenshot(ScreenShotType.LanguageChange, 0, driver);
                driver.Quit();
                throw new Exception("Unexpected amount of languages");
            }
        }
        /// <summary>
        /// Check if input string equals one of the string in array.
        /// </summary>
        /// <param name="inputString">String to check with others</param>
        /// <param name="stringArr">Array of strings to check with the input</param>
        /// <returns>True if input string equals one of the string in array</returns>
        static bool checkStringContains(string inputString, string[] stringArr)
        {
            int countArr = stringArr.Count();
            bool result = false;
            for (int i = 0; i < countArr; i++)
            {
                result = result || inputString.Contains(stringArr[i]);
            }
            return result;
        }
    }
}
