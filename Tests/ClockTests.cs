using NUnit.Framework;

namespace WinAppTest.Tests
{
    [TestFixture]
    public class ClockTests : BaseTest
    {
        private readonly string cityName = "Warsaw";

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            Page.ClockPage.AddNewCity(cityName);
        }

        [Test]
        public void DeleteCityTest()
        {
            Page.ClockPage.DeleteCity(cityName);
            var isPresent = Page.ClockPage.IsCityDisplayed(cityName);
            Assert.IsFalse(isPresent, "City is displayed after deletion");
        }
    }
}
