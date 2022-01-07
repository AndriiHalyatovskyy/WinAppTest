using OpenQA.Selenium;

namespace WinAppTest.Pages.CustomApp
{
    public class CustomAppPage : APage<CustomAppPageSelectors>
    {
        public CustomAppPage(Page p) : base(p, new CustomAppPageSelectors())
        {
        }

        /// <summary>
        /// Click on checkbox by index
        /// </summary>
        /// <param name="checkBoxNumber">CheckBox index</param>
        public void ClickCheckbox(int checkBoxNumber)
        {
            page.Click(selectors.GetCheckBox(checkBoxNumber));
        }

        /// <summary>
        /// Click on radio button by name
        /// </summary>
        /// <param name="name">Radio button name</param>
        public void ClickOnRadioButton(string name)
        {
            page.Click(selectors.GetRadioButton(name));
        }

        /// <summary>
        /// Open combo box dropdown
        /// </summary>
        public void OpenComboBox()
        {
            page.Click(selectors.ComboBoxOpenButton);
        }

        /// <summary>
        /// Selects item from combo box dropdown
        /// </summary>
        /// <param name="option">Item to select</param>
        public void OpenAndSelectfromComboBox(string option)
        {
            OpenComboBox();
            page.Click(selectors.GetOptionFromComboBox(option));
        }

        public void CreateNewFile(string name)
        {
            page.Click(selectors.GetElementByName("File"));
            page.Click(selectors.GetElementByName("New"));
            page.Click(selectors.GetElementByName(name));
        }
    }

    public class CustomAppPageSelectors
    {
        public By GetCheckBox(int number) => By.Name($"checkBox{number}");
        public By GetRadioButton(string name) => By.XPath($"//RadioButton[@Name = '{name}']");
        public By GetOptionFromComboBox(string name) => By.XPath($"//ListItem[@Name = '{name}']");
        public By GetElementByName(string name) => By.XPath($"//*[@Name = '{name}']");

        public By ComboBoxOpenButton = By.XPath("//ComboBox//Button[@Name = 'Open']");
    }
}
