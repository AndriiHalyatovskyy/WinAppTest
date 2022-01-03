using NUnit.Framework;

namespace WinAppTest.Tests
{
    [TestFixture]
    public class NotepadTest : BaseTest
    {

        [Test]
        public void CalcFirstTest()
        {
            Page.CalcPage.SendKeysAndCalculate("2", "+", "6");
            var res = Page.CalcPage.GetCalculationResult();

            Assert.AreEqual("Display is 8", res);
        }
    }
}
