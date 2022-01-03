using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using WinAppTest.Pages.Calc;

namespace WinAppTest.Pages
{
    public class Page
    {
        private const int DEFAULT_WAIT = 20;
        public WindowsDriver<WindowsElement> driver;
        public WebDriverWait Wait;

        private CalcPage calcPage;

        public Page(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DEFAULT_WAIT));
        }

        public CalcPage CalcPage
        {
            get { return calcPage ?? (calcPage = new CalcPage(this)); }
        }

        /// <summary>
        /// Waits for element to be present and visible and then clicks it
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scroll"></param>
        /// <param name="scrollDistance"></param>
        /// <param name="scrollToTop"></param>
        public AppiumWebElement Click(By element, ScrollOptions scroll = ScrollOptions.none, int scrollDistance = 50, bool scrollToTop = false)
        {
            WaitForElementPresent(element);
            //if (scroll != ScrollOptions.none)
            // ScrollToElement(element, scroll, scrollDistance, scrollToTop);
            WaitForEnabled(element);
            return JustClick(element);
        }

        /// <summary>
        /// Just clicks an element. Does not wait for it to be present and visible.
        /// </summary>
        /// <param name="element"></param>
        public AppiumWebElement JustClick(By element)
        {
            var el = driver.FindElement(element);
            el.Click();
            return el;
        }

        /// <summary>
        /// Waits untill element is not present on page
        /// </summary>
        /// <param name="element"></param>
        public void WaitForElementPresent(By element)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(element));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Waits for the element to be enabled and clickable
        /// </summary>
        /// <param name="elements"></param>
        public void WaitForEnabled(params By[] elements)
        {
            foreach (var element in elements)
            {
                try
                {
                    Wait.Until(ExpectedConditions.ElementToBeClickable(element));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Sends keys to element
        /// </summary>
        /// /// <param name="element"></param>
        /// /// <param name="keys"></param>
        public void SendKeys(By element, params string[] keys)
        {
            Array.ForEach(keys, key => driver.FindElement(element).SendKeys(key));
        }

        /// <summary>
        /// Fails test. Throws 'AssertionException' with a specified fail message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="failMessage">Message to be displayed in test results.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public void FailIt(string failMessage, Exception inner)
        {
            throw new AssertionException(failMessage, inner);
        }

        /// <summary>
        /// Waits for element(s) to be visible
        /// </summary>
        /// <param name="elements">Element(s) to wait for</param>
        public void WaitForVisible(params By[] elements)
        {
            foreach (var element in elements)
            {
                try
                {
                    //IE was giving problems without this
                    //I know there is a waitforboth but this should increase IE test success rate
                    Wait.Until(ExpectedConditions.ElementExists(element));
                    Wait.Until(ExpectedConditions.ElementIsVisible(element));
                }
                catch (WebDriverTimeoutException e)
                {
                    FailIt("Timed out after " + Wait.Timeout + " seconds waiting for " + element.ToString() + " to be visible.", e.InnerException);
                }
            }
        }

        /// <summary>
        /// Returns the value for the given html attribute
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attName"></param>
        /// <param name="waitForVisible">True (default) = wait for element visible, false = do not wait for element visible</param>
        /// <returns></returns>
        public string GetAttributeValue(By element, string attName, bool waitForVisible = true)
        {
            WaitForElementPresent(element);
            if (waitForVisible)
            {
                WaitForVisible(element);
            }
            return driver.FindElement(element).GetAttribute(attName);
        }

        /// <summary>
        /// Gets the text of an element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public string GetText(By element)
        {
            return driver.FindElement(element).Text;
        }
    }

    public enum ScrollOptions
    {
        none,
        up,
        down,
        intoView
    }
}
