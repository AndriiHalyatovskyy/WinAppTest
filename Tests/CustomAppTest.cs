using NUnit.Framework;

namespace WinAppTest.Tests
{
    [TestFixture]
    public class CustomAppTest : BaseTest
    {

        [Test]
        public void DropDownTest()
        {
            Page.CustomAppPage.ClickOnRadioButton("Third");
            Page.CustomAppPage.OpenAndSelectfromComboBox("CO");
            Page.CustomAppPage.CreateNewFile("Second");
        }
    }
}
