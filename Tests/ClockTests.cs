using NUnit.Framework;

namespace WinAppTest.Tests
{
    [TestFixture]
    public class ClockTests : BaseTest
    {

        [Test]
        public void DeleteCityTest()
        {
            Page.ClockPage.DeleteCity("Wars");
        }
    }
}
