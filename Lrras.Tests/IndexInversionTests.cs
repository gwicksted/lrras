using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class IndexInversionTests
    {
        [Test]
        public void TestInvertZero()
        {
            Assert.That(Scalar.IndexInversion(0), Is.EqualTo(0));
        }

        [Test]
        public void TestInvertOne()
        {
            Assert.That(Scalar.IndexInversion(1), Is.EqualTo(1));
        }
        
        [Test]
        public void TestInvertTwo()
        {
            Assert.That(Scalar.IndexInversion(2), Is.EqualTo(-1));
        }
        
        [Test]
        public void TestInvertNegativeOne()
        {
            Assert.That(Scalar.IndexInversion(-1), Is.EqualTo(2));
        }
    }
}