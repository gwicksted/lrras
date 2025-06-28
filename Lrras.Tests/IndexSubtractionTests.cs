using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class IndexSubtractionTests
    {
        [Test]
        public void TestSubtractZero()
        {
            Assert.That(Scalar.IndexSubtract(0, 0), Is.EqualTo(0));
        }

        [Test]
        public void TestSubtractOne()
        {
            Assert.That(Scalar.IndexSubtract(1, 1), Is.EqualTo(1));
        }

        [Test]
        public void TestSubtractOneZero()
        {
            Assert.That(Scalar.IndexSubtract(0, 1), Is.EqualTo(1));
            Assert.That(Scalar.IndexSubtract(1, 0), Is.EqualTo(1));
        }

        [Test]
        public void TestSubtractTwo()
        {
            Assert.That(Scalar.IndexSubtract(2, 2), Is.EqualTo(1));
        }
        
        [Test]
        public void TestSubtractNegativeOne()
        {
            Assert.That(Scalar.IndexSubtract(-1, -1), Is.EqualTo(1));
        }

        [Test]
        public void TestSubtractNegativeOneZero()
        {
            Assert.That(Scalar.IndexSubtract(-1, 0), Is.EqualTo(-1));
            Assert.That(Scalar.IndexSubtract(0, -1), Is.EqualTo(2));
        }
        

        [Test]
        public void TestSubtractTwoZero()
        {
            Assert.That(Scalar.IndexSubtract(2, 0), Is.EqualTo(2));
            Assert.That(Scalar.IndexSubtract(0, 2), Is.EqualTo(-1));
        }
    }
}