using NUnit.Framework;
using System.Numerics;

namespace Lrras.Tests
{
    [TestFixture]
    public class Rot270Tests
    {
        [Test]
        public void TestRotate0()
        {
            Assert.That(new Complex(0, 0).Rot270(), Is.EqualTo(new Complex(0, -0)));
        }

        [Test]
        public void TestRotateNegative1()
        {
            Assert.That(new Complex(-1, 0).Rot270(), Is.EqualTo(new Complex(0, 1)));
        }
        
        [Test]
        public void TestRotateImaginary1()
        {
            Assert.That(new Complex(0, 1).Rot270(), Is.EqualTo(new Complex(1, -0)));
        }
        
        [Test]
        public void TestRotateImaginaryNegative1()
        {
            Assert.That(new Complex(0, -1).Rot270(), Is.EqualTo(new Complex(-1, -0)));
        }
        
        [Test]
        public void TestRotateComplexPositive()
        {
            Assert.That(new Complex(2, 4).Rot270(), Is.EqualTo(new Complex(4, -2)));
        }
        
        [Test]
        public void TestRotateComplexNegative()
        {
            Assert.That(new Complex(-2, -4).Rot270(), Is.EqualTo(new Complex(-4, 2)));
        }
        
        [Test]
        public void TestReturnTo0()
        {
            // A 270-degree rotation is 90-degrees from a 360-degree rotation
            // So 270 + 270 % 360 = 180 (negation)
            // And 270 + 270 + 270 % 360 = 90 (imaginary)
            // And 270 + 270 + 270 + 270 % 360 = 0 (return to original position)
            Assert.That(new Complex(-2, -4).Rot270().Rot270().Rot270().Rot270(), Is.EqualTo(new Complex(-2, -4)));
        }
    }
}
