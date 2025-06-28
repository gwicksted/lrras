using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class SignPreservingSqrtTests
    {
        [Test]
        public void SqrtNegativeOne()
        {
            Assert.That(new Complex(-1, 0).Ssqrt(), Is.EqualTo(new Complex(0, 1)));
        }
        

        [Test]
        public void TestNegativeInfinityMinusOneSqrt()
        {
            var s = Scalar.Create(-1, -1);

            Assert.That(Scalar.Create(s.V.Sisqrt(), s.I), Is.EqualTo(Scalar.Create(new Complex(0, -1), -1)));
        }
    }
}
