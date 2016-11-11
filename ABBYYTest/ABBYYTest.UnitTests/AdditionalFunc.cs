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
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Remote;

namespace ABBYYTest.UnitTests
{
    class AdditionalFunc
    {
        // Image path to images folder in the solution directory.
        static string imagePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\images\\";
        // ABBYY contact phone.
        public static string phone = "+7 (495) 374-56-08";

        public static string engSite = "abbyy-ls.com";
        public static string deSite = "abbyy-ls.de";
        public static string uaSite = "abbyy-ls.ua";
        public static string kzSite = "abbyy-ls.kz";
        public static string[] langList = { "abbyy-ls.com", "abbyy-ls.de", "abbyy-ls.ua", "abbyy-ls.kz" };

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
        /// Take a screenshot of main page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="imgNumber">Number of current image if >= 0
        /// If -1: problem with header (language / phone).
        /// </param>
        /// <param name="driver">IWebDriver</param>
        public static void takeScreenshotMainPage(int imgNumber, IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            checkImageDirectory(imagePath, driver);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            if (imgNumber >= 0)
                scrFile.SaveAsFile(imagePath + ++imgNumber + "Menu" + capabilities.BrowserName + "WrongImageOnMainPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            else
                scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnMainPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        /// <summary>
        /// Take sreenshot of caculator page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void takeScreenShotCalcPage(IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            checkImageDirectory(imagePath, driver);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnCalculatorPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        /// <summary>
        /// Take screenshot of interpret offer page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void takeScreenShotInterpPage(IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            checkImageDirectory(imagePath, driver);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnInterpretOfferPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        /// <summary>
        /// Take screenshot of a page and save it in images folder of solution directory.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void takeScreenShot(IWebDriver driver)
        {
            ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
            checkImageDirectory(imagePath, driver);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(imagePath + capabilities.BrowserName + "ErrorOnPage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// Check text of phone element.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        public static void checkPhoneText(IWebDriver driver)
        {
            try
            {
                Assert.IsTrue(driver.FindElement(By.ClassName("call_phone_1")).Text.Equals(AdditionalFunc.phone));
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShot(driver);
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
                Assert.IsTrue(driver.FindElements(By.ClassName("lang-switcher")).Count != 0);
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShot(driver);
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
                IList<IWebElement> list = driver.FindElements(By.ClassName("lang-switcher__item"));
                checkLangListCount(list, driver);
                Assert.IsTrue((checkStringContains(list[0].GetAttribute("href"), AdditionalFunc.langList) &&
                    checkStringContains(list[1].GetAttribute("href"), AdditionalFunc.langList) &&
                    checkStringContains(list[2].GetAttribute("href"), AdditionalFunc.langList) &&
                    checkStringContains(list[2].GetAttribute("href"), AdditionalFunc.langList)));
            }
            catch (AssertionException)
            {
                driver.FindElement(By.ClassName("lang-switcher")).Click();
                AdditionalFunc.takeScreenShot(driver);
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
                driver.FindElement(By.ClassName("lang-switcher")).Click();
                AdditionalFunc.takeScreenShot(driver);
                driver.Quit();
                throw new Exception("Unexpected amount of languages");
            }
        }
        /// <summary>
        /// Check if input string equals one of the string in array.
        /// </summary>
        /// <param name="inputString">String to check with others</param>
        /// <param name="stringArr">Array of strings to check with the input</param>
        /// <returns></returns>
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
