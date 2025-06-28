using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class IndexDivisionTests
    {
        // repeated division is always 1
        [Test]
        public void TestRepeated()
        {
            for (var i = -1; i <= 2; i++)
            {
                Assert.That(Scalar.IndexDivide(i, i), Is.EqualTo(1));
            }
        }
    }
}