using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace WinAppTest.Pages.Calc
{
    public class CalcPage : APage<CalcPageSelectors>
    {
        public CalcPage(Page p) : base(p, new CalcPageSelectors())
        {
        }

        /// <summary>
        /// Clicks on number button
        /// </summary>
        /// <param name="button">Button to click</param>
        public void ClickNumberButton(int button)
        {
            page.Click(selectors.GetNumButton(button));
        }

        /// <summary>
        /// Clicks on plus button
        /// </summary>
        public void ClickPlus()
        {
            page.Click(selectors.PlusButton);
        }

        /// <summary>
        /// Clicks on minus button
        /// </summary>
        public void ClickMinus()
        {
            page.Click(selectors.MinusButton);
        }

        /// <summary>
        /// Clicks on equal button
        /// </summary>
        public void ClickEqual()
        {
            page.Click(selectors.EqualButton);
        }

        /// <summary>
        /// Sends array of keys to app
        /// </summary>
        /// <param name="keys"></param>
        public void SendKeysAndCalculate(params string [] keys)
        {
            page.SendKeys(selectors.ResultField, keys);
            page.SendKeys(selectors.ResultField, Keys.Enter);
        }

        /// <summary>
        /// Sends array of keys to app. Keys should be separated by whitespace
        /// </summary>
        /// <param name="keys"></param>
        public void SendKeysAndCalculate(string keys)
        {
            var keyArr = keys.Split(' ');
            page.SendKeys(selectors.ResultField, keys);
            page.SendKeys(selectors.ResultField, Keys.Enter);
        }

        /// <summary>
        /// Returns result of calculation
        /// </summary>
        public string GetCalculationResult()
        {
            return page.GetText(selectors.ResultField);
        }
    }

    public class CalcPageSelectors
    {
        public By GetNumButton(int button) => MobileBy.AccessibilityId($"num{button}Button");
        public By PlusButton = MobileBy.AccessibilityId("plusButton");
        public By MinusButton = MobileBy.AccessibilityId("minusButton");
        public By EqualButton = By.Name("Equals");
        public By ResultField = MobileBy.AccessibilityId("CalculatorResults");
    }
}
