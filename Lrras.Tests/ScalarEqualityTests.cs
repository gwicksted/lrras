using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarEqualityTests
    {
        [Test]
        public void TestStoresComplex()
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var c = new Complex(i, j);

                    Assert.That(Scalar.Create(c).Evaluate(), Is.EqualTo(c));
                }
            }
        }
        
        [Test]
        public void ImplicitConversion()
        {
            var c = new Complex(2, 0);

            Assert.That(Scalar.Create(c) + c, Is.EqualTo(new Complex(4, 0)));
        }
        
        [Test]
        public void ZeroEquality()
        {
            var s = Scalar.Create(200, 0);

            Assert.That((int)s == 0, Is.True);
        }
        
        [Test]
        public void ZeroInequality()
        {
            var s1 = Scalar.Create(200, 0);
            var s2 = Scalar.Create(2, 0);

            Assert.That(s1 != s2, Is.True);
            Assert.That(s1.Evaluate(), Is.EqualTo(s2.Evaluate()));
        }

        [Test]
        public void InfinityEquality()
        {
            var s1 = Scalar.Create(Complex.Infinity);
            var s2 = Scalar.Infinity;

            Assert.That(s1 == Complex.Infinity, Is.True);
            Assert.That(s1.V, Is.EqualTo(new Complex(1, 0)));
            Assert.That(s1.I, Is.EqualTo(2));
            Assert.That(s1 == s2, Is.True);

            s1 = Scalar.Create(-Complex.Infinity);
            s2 = Scalar.NegativeInfinity;

            Assert.That(s1 == -Complex.Infinity, Is.True);
            Assert.That(s1.V, Is.EqualTo(new Complex(1, 0)));
            Assert.That(s1.I, Is.EqualTo(-1));
            Assert.That(s1 == s2, Is.True);
        }
        
        [Test]
        public void InfinityInequality()
        {
            var a = Scalar.Create(1, 2);
            var b = Scalar.Create(100, 2);

            var na = Scalar.Create(1, -1);
            var nb = Scalar.Create(100, -1);

            Assert.That(a != b, Is.True);
            Assert.That(na != a, Is.True);
            Assert.That(nb != b, Is.True);
            Assert.That(na != nb, Is.True);
        }

        [Test]
        public void TestNaN()
        {
            Assert.Throws<ArgumentException>(() => Scalar.Create(Complex.NaN));
        }
    }
}
