using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class GeometricNegativeTests
    {
        [Test]
        public void TestRealNegative()
        {
            Assert.That(new Complex(-1, 0).IsGeometricNegative(), Is.True);
        }

        [Test]
        public void TestZero()
        {
            Assert.That(new Complex(0, 0).IsGeometricNegative(), Is.False);
        }
        
        [Test]
        public void TestImaginaryNegative()
        {
            Assert.That(new Complex(0, -1).IsGeometricNegative(), Is.True);
        }
        
        [Test]
        public void TestImaginaryPositive()
        {
            Assert.That(new Complex(0, 1).IsGeometricNegative(), Is.False);
        }

        [Test]
        public void TestRealPositiveImaginaryPositive()
        {
            Assert.That(new Complex(1, 1).IsGeometricNegative(), Is.False);
        }


        [Test]
        public void TestRealPositiveImaginaryNegative()
        {
            Assert.That(new Complex(40, -2).IsGeometricNegative(), Is.True);
        }

        [Test]
        public void TestRealNegativeImaginaryPositive()
        {
            Assert.That(new Complex(-1, 25).IsGeometricNegative(), Is.True);
        }
    }
}
