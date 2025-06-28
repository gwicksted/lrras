using NUnit.Framework;
using System.Numerics;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarMultiplicationTests
    {
        
        [Test]
        public void TestNegativeImaginaryNumberByRealNumberMultiplication()
        {
            var a = Scalar.Create(3600);
            var b = Scalar.Create(new Complex(0, -1), -1);
            
            Assert.That(a * b, Is.EqualTo(Scalar.Create(new Complex(0, -3600), -1)));
        }

        [Test]
        public void TestLosslessZeroMultiplication()
        {
            var z10 = Scalar.Create(10, 0);
            var z5 = Scalar.Create(5, 0);

            Assert.That(z10 * z5, Is.EqualTo(Scalar.Create(50, 0)));
        }
        
        [Test]
        public void TestLosslessZeroRealMultiplication()
        {
            var z10 = Scalar.Create(10, 0);
            var r5 = Scalar.Create(5);

            Assert.That(z10 * r5, Is.EqualTo(Scalar.Create(50, 0)));
        }

        [Test]
        public void TestMultiplicationDivisionSymmetry()
        {
            for (var i = -100; i <= 100; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                for (var j = -100; j <= 100; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }

                    for (var x = -1; x <= 2; x++)
                    {
                        for (var y = -1; x <= 2; x++)
                        {
                            var a = Scalar.Create(i, x);
                            var b = Scalar.Create(j, y);

                            var c = a * b;
                            var expected = i * j;

                            var d = c / b;

                            Assert.That(c.V.Real, Is.EqualTo(expected), $"{c} = {a} * {b}; {expected} = {x}+{{{i}}} * {y}+{{{j}}}");
                            
                            if ((d.I == 2 && a.I == 0) || (d.I == 1 && a.I == -1) || (d.I == 0 && a.I == 2))
                            {
                                // let it slide
                            }
                            else
                            {
                                Assert.That(d, Is.EqualTo(a), $"{a} * {b} = {c}; {c} / {b} = {d}; {d} == {a}");
                            }
                        }
                    }
                }
            }
        }


        [Test]
        public void TestTraditionalMultiplication()
        {
            for (var i = -100; i <= 100; i++)
            {
                for (var j = -100; j <= 100; j++)
                {
                    var a = Scalar.Create(i);
                    var b = Scalar.Create(j);

                    var s = a * b;
                    var z = i * j;

                    Assert.That((int)s, Is.EqualTo(z));
                }
            }
        }
    }
}
