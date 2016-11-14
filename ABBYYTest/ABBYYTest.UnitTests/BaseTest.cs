using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using ABBYYTest;

namespace ABBYYTest.UnitTests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class BaseTest <TIWebDriver> where TIWebDriver: IWebDriver, new()
    {
        public IWebDriver webDriver;

        public BaseTest(string url)
        {
           this.webDriver = new TIWebDriver();
           this.webDriver.Manage().Window.Maximize();
           this.webDriver.Navigate().GoToUrl(url);
            ABBYYTest.BasePage.checkPhoneText(this.webDriver);
            ABBYYTest.BasePage.checkLangSwitcherExistence(this.webDriver);
            ABBYYTest.BasePage.checkLangSwitcherElements(this.webDriver);            
            //checkPhoneText(this.webDriver);
        }

        /// <summary>
        /// Quit the current IWebDriver.
        /// </summary>
        public void Dispose()
        {
            this.webDriver.Quit();
        }

        
        static void testContactInfo()
        {

        }
    }

}
