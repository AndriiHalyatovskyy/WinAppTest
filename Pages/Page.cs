using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using WinAppTest.Pages.Calc;

namespace WinAppTest.Pages
{
    public class Page
    {
        private const int DEFAULT_WAIT = 20;
        public AndroidDriver<AndroidElement> driver;
        public WebDriverWait Wait;

        private CalcPage calcPage;

        public Page(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DEFAULT_WAIT));
        }

        public CalcPage CalcPage
        {
            get { return calcPage ?? (calcPage = new CalcPage(this)); }
        }
    }
}
