using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using NUnit.Framework;
using ABBYYTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;


namespace ABBYYTest.UnitTests
{
    [TestFixture]
    public class UnitTestCalcPage
    {
        private static CalculatorPage calcPage;
        private static IWebDriver driver;
        /// <summary>
        /// Chrome test.
        /// </summary>
        [Test]
        public void chromeCalcPageTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            testWrittenTranslation(driver);
            driver.Quit();
        }
        /// <summary>
        /// Firefox test.
        /// </summary>
        [Test]
        public void firefoxCalcPageTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            testWrittenTranslation(driver);
            driver.Quit();
        }
        /// <summary>
        /// Internet explorer test.
        /// </summary>
        [Test]
        public void ieCalcPageTest()
        {
            driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            testWrittenTranslation(driver);
            driver.Quit();
        }

        /// <summary>
        /// Test if 'from lang' and 'to lang' dropboxes are not empty.
        /// In the first there should be available option 'Русский',
        /// so that in the second one option 'Английский' is available.
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        static void testWrittenTranslation(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(CalculatorPage.url);
            calcPage = new CalculatorPage(driver);
            IWebElement langDropBox = null;
            IList<IWebElement> langOptions = null;
            checkFromLangDropbox(ref langDropBox, ref langOptions);
            checkFromLangOptions(langDropBox, langOptions);
            // Wait until previous action is correcty completed in browser.
            Thread.Sleep(500);
            checkToLangDropbox(ref langDropBox, ref langOptions);
            checkToLangOptions(langDropBox, langOptions);
            AdditionalFunc.checkPhoneText(driver);
            AdditionalFunc.checkLangSwitcherExistence(driver);
            AdditionalFunc.checkLangSwitcherElements(driver);
        }

        /// <summary>
        /// Check if 'from lang' dropbox is not empty.
        /// </summary>
        /// <param name="lang">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        static void checkFromLangDropbox(ref IWebElement dropBox, ref IList<IWebElement> langOptions)
        {
            try
            {
                dropBox = calcPage.getFromLangElement();
                dropBox.Click();
                langOptions = calcPage.getFromLangOptions();
                Assert.IsTrue(langOptions.Count >= 1);
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShotCalcPage(driver);
                driver.Quit();
                throw new AssertionException("'from language' dropbox is empty");
            }
        }

        /// <summary>
        /// Check if 'from lang' droppbox contains option 'Русский'.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        static void checkFromLangOptions(IWebElement dropBox, IList<IWebElement> langOptions)
        {
            int optionsCount = langOptions.Count;
            for (int dropBoxOption = 0; dropBoxOption < optionsCount; dropBoxOption++)
            {
                IWebElement option = langOptions[dropBoxOption];
                if (option.GetAttribute("text").Equals("Русский"))
                {
                    if (((RemoteWebDriver)driver).Capabilities.BrowserName.Equals("firefox"))
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
                    {
                        SelectElement selectEl = new SelectElement(dropBox);
                        selectEl.SelectByIndex(dropBoxOption);
                    }
                    return;
                }
            }
            AdditionalFunc.takeScreenShotCalcPage(driver);
            driver.Quit();
            throw new AssertionException("'Русский' is not found in the 'from language' dropbox");
        }

        /// <summary>
        /// Check if 'to lang' dropbox is not empty.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        static void checkToLangDropbox(ref IWebElement dropBox, ref IList<IWebElement> langOptions)
        {
            try
            {
                dropBox = calcPage.getToLangElement();
                dropBox.Click();
                langOptions = calcPage.getToLangOptions();
                Assert.IsTrue(langOptions.Count >= 1);
            }
            catch (AssertionException)
            {
                AdditionalFunc.takeScreenShotCalcPage(driver);
                driver.Quit();
                throw new AssertionException("'to language' dropbox is empty");
            }
        }

        /// <summary>
        /// Check if 'to lang' droppbox contains option 'Английский'.
        /// </summary>
        /// <param name="dropBox">IWebElement of dropbox</param>
        /// <param name="langOptions">Ilist of options of dropbox</param>
        static void checkToLangOptions(IWebElement dropBox, IList<IWebElement> langOptions)
        {
            int optionsCount = langOptions.Count;
            for (int i = 0; i < optionsCount; i++)
            {
                IWebElement option = langOptions[i];
                if (option.GetAttribute("text").Equals("Английский"))
                {
                    SelectElement selectEl = new SelectElement(dropBox);
                    selectEl.SelectByIndex(i);
                    return;
                }
            }
            AdditionalFunc.takeScreenShotCalcPage(driver);
            driver.Quit();
            throw new AssertionException("'Английский' is not found in the 'to language' dropbox");
        }
    }
}
