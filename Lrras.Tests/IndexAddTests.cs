using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class IndexAddTests
    {
        [Test]
        public void TestAddZero()
        {
            Assert.That(Scalar.IndexAdd(0, 0), Is.EqualTo(0));
        }

        [Test]
        public void TestAddOne()
        {
            Assert.That(Scalar.IndexAdd(1, 1), Is.EqualTo(1));
        }

        [Test]
        public void TestAddOneZero()
        {
            Assert.That(Scalar.IndexAdd(0, 1), Is.EqualTo(1));
            Assert.That(Scalar.IndexAdd(1, 0), Is.EqualTo(1));
        }

        [Test]
        public void TestAddTwo()
        {
            Assert.That(Scalar.IndexAdd(2, 2), Is.EqualTo(2));
        }
        
        [Test]
        public void TestAddNegativeOne()
        {
            Assert.That(Scalar.IndexAdd(-1, -1), Is.EqualTo(-1));
        }

        [Test]
        public void TestAddNegativeOneZero()
        {
            Assert.That(Scalar.IndexAdd(-1, 0), Is.EqualTo(-1));
            Assert.That(Scalar.IndexAdd(0, -1), Is.EqualTo(-1));
        }
        

        [Test]
        public void TestAddTwoZero()
        {
            Assert.That(Scalar.IndexAdd(2, 0), Is.EqualTo(2));
            Assert.That(Scalar.IndexAdd(0, 2), Is.EqualTo(2));
        }

    }
}