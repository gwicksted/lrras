using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarAdditionTests
    {
        [Test]
        public void TestOneMinusInfinity()
        {
            var a = Scalar.One;
            var b = Scalar.Infinity;

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, -1)));
        }
        
        [Test]
        public void TestLossyZeroAddition()
        {
            var a = Scalar.Create(150, 0);
            var b = Scalar.One;

            Assert.That(a + b, Is.EqualTo(Scalar.One));
        }
        
        [Test]
        public void TestLosslessZeroCounting()
        {
            var a = Scalar.Zero;
            var b = Scalar.Zero;

            Assert.That(a + b, Is.EqualTo(Scalar.Create(2, 0)));
        }
        
        [Test]
        public void TestLosslessInfinityRealAddition()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Create(1);

            Assert.That(a + b, Is.EqualTo(Scalar.Create(2, 2)));
        }
        
        [Test]
        public void TestInfinityCounting()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Infinity;

            Assert.That(a + b, Is.EqualTo(Scalar.Create(2, 2)));
        }
        
        [Test]
        public void TestLossyInfinityZeroAddition()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Zero;

            Assert.That(a + b, Is.EqualTo(Scalar.Create(1, 2)));
        }
        
        [Test]
        public void TestLossyZeroInfinityAddition()
        {
            var a = Scalar.Zero;
            var b = Scalar.Infinity;

            Assert.That(a + b, Is.EqualTo(Scalar.Create(1, 2)));
        }

        [Test]
        public void TestLosslessNegativeInfinityRealAddition()
        {
            var a = Scalar.NegativeInfinity;
            var b = Scalar.Create(1);

            Assert.That(a + b, Is.EqualTo(Scalar.Create(2, -1)));
        }
        
        [Test]
        public void TestNegativeInfinityCounting()
        {
            var a = Scalar.NegativeInfinity;
            var b = Scalar.NegativeInfinity;

            Assert.That(a + b, Is.EqualTo(Scalar.Create(2, -1)));
        }
        
        
        [Test]
        public void TestLossyNegativeInfinityZero()
        {
            var a = Scalar.NegativeInfinity;
            var b = Scalar.Create(5, 0);

            Assert.That(a + b, Is.EqualTo(Scalar.NegativeInfinity));
        }
        
        
        [Test]
        public void TestLossyZeroNegativeInfinity()
        {
            var a = Scalar.Create(5, 0);
            var b = Scalar.NegativeInfinity;

            Assert.That(a + b, Is.EqualTo(Scalar.NegativeInfinity));
        }

        [Test]
        public void TestLosslessInfinityInfinityAddition()
        {
            var a = Scalar.Create(3, 2);
            var b = Scalar.Create(5, 2);

            Assert.That(a + b, Is.EqualTo(Scalar.Create(8, 2)));
        }

        [Test]
        public void TestTraditionalAddition()
        {
            for (var i = -100; i <= 100; i++)
            {
                for (var j = -100; j <= 100; j++)
                {
                    var a = Scalar.Create(i);
                    var b = Scalar.Create(j);

                    var s = a + b;
                    var z = i + j;

                    Assert.That((int)s, Is.EqualTo(z));
                }
            }
        }

        
        [Test]
        public void TestAddOpposingPositiveInfinities()
        {
            var a = Scalar.Create(1, 2);
            var b = Scalar.Create(-1, 2);

            Assert.That(a + b, Is.EqualTo(Scalar.Zero));
        }
        
        [Test]
        public void TestAddDifferentPositiveInfinities()
        {
            var a = Scalar.Create(12, 2);
            var b = Scalar.Create(-15, 2);

            Assert.That(a + b, Is.EqualTo(Scalar.Create(-3)));
        }
    }
}
