using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class IndexMultiplicationTests
    {
        [Test]
        public void TestCommutativeProperty()
        {
            for (var i = -1; i <= 2; i++)
            {
                for (var j = -1; j <= 2; j++)
                {
                    Assert.That(Scalar.IndexMultiply(i, j), Is.EqualTo(Scalar.IndexMultiply(j, i)));
                }
            }
        }
        
        // repeated multiplication is always the same space
        [Test]
        public void TestRepeated()
        {
            for (var i = -1; i <= 2; i++)
            {
                var r = Scalar.IndexMultiply(i, i);

                Assert.That(r, Is.EqualTo(i));
            }
        }

    }
}