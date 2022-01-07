using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppTest.Pages.Clock
{
    public class ClockPage : APage<ClockPageSelectors>
    {
        public ClockPage(Page p) : base(p, new ClockPageSelectors())
        {
        }


        /// <summary>
        /// Clicks on add new city button
        /// </summary>
        public void ClickAddNewCity()
        {
            page.Click(selectors.AddNewCity);
        }

        /// <summary>
        /// Types city location
        /// </summary>
        /// <param name="city">City name</param>
        public void TypeCityLocation(string city)
        {
            page.SendKeys(selectors.AddNewCityInput, city);
        }

        /// <summary>
        /// Adds new city
        /// </summary>
        /// <param name="city">City location</param>
        public void AddNewCity(string city)
        {
            ClickAddNewCity();
            TypeCityLocation(city);
            page.SendKeys(selectors.AddNewCityInput, Keys.Enter);
        }

        /// <summary>
        /// Deletes city by name
        /// </summary>
        /// <param name="city"></param>
        public void DeleteCity(string city)
        {
            page.RightClickOnElement(selectors.GetCityCart(city));
            ClickDelete();
        }

        /// <summary>
        /// Clicks on delete butoon
        /// </summary>
        public void ClickDelete()
        {
            page.Click(selectors.DeleteButton);
        }

        /// <summary>
        /// Returns true if city present on the board
        /// </summary>
        /// <param name="cityName">City name</param>
        public bool IsCityDisplayed(string cityName)
        {
            return page.IsElementPresent(selectors.GetCityCart(cityName)) && page.IsElementVisible(selectors.GetCityCart(cityName));
        }
    }

    public class ClockPageSelectors
    {
        public By GetCityCart(string city) => By.XPath($"//Group[contains(@Name, '{city}')]");
        public By AddNewCity = MobileBy.AccessibilityId("AddClockButton");
        public By AddNewCityInput = MobileBy.AccessibilityId("AddClockAutoSuggestBox");
        public By DeleteButton = MobileBy.AccessibilityId("ContextMenuDelete");
    }
}
