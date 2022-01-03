using NUnit.Framework;

namespace WinAppTest.Tests
{
    [TestFixture]
    public class CalcTests : BaseTest
    {

        [Test]
        public void CalcFirstTest()
        {
            Page.CalcPage.SendKeysAndCalculate("2", "+", "6");
            var res = Page.CalcPage.GetCalculationResult();

            Assert.AreEqual("Display is 8", res);
        }

        [Test]
        public void OtherTest()
        {
            Page.CalcPage.SendKeysAndCalculate("( 2 + 1 ) * 3");
            var res = Page.CalcPage.GetCalculationResult();
            Assert.AreEqual("Display is 9", res);
        }
    }
}
