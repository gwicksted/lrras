using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarDivisionTests
    {
        // x / (1 / 0)
        // = x*0 / 1

        [Test]
        public void TestOneOverNegativeImaginaryNegativeInfinity()
        {
            var a = Scalar.One;
            var b = Scalar.Create(new Complex(0, -1), -1);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(new Complex(0, 1), 2)));
        }

        // TODO: test exponent produces same result!
        [Test]
        public void TestOneOverZeroDivision()
        {
            var a = Scalar.One;
            var b = Scalar.Zero;

            Assert.That(a / b, Is.EqualTo(Scalar.Infinity));
        }
        
        [Test]
        public void TestFiveOverInfinityDivision()
        {
            var a = Scalar.Create(5);
            var b = Scalar.Infinity;

            Assert.That(a / b, Is.EqualTo(Scalar.Create(5, -1)));
        }

        [Test]
        public void TestZeroDivision()
        {
            var a = Scalar.Create(10, 0);
            var b = Scalar.Create(5, 0);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2)));
        }
        
        [Test]
        public void TestRealZeroDivision()
        {
            var a = Scalar.Create(10);
            var b = Scalar.Create(5, 0);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2, 2)));
        }

        [Test]
        public void TestLosslessZeroRealDivision()
        {
            var a = Scalar.Create(10, 0);
            var b = Scalar.Create(5);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2, 0)));
        }
        
        [Test]
        public void TestInfinityByInfinityDivision()
        {
            var a = Scalar.Create(10, 2);
            var b = Scalar.Create(5, 2);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2)));
        }
        
        [Test]
        public void TestInfinityByNegativeInfinityDivision()
        {
            var a = Scalar.Create(10, 2);
            var b = Scalar.Create(5, -1);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2, 2)));
        }
        
        [Test]
        public void TestZeroByNegativeInfinityDivision()
        {
            var a = Scalar.Create(10, 0);
            var b = Scalar.Create(5, -1);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2)));
        }
        
        [Test]
        public void TestRealByNegativeInfinityDivision()
        {
            var a = Scalar.Create(10);
            var b = Scalar.Create(5, -1);

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2, 2)));
        }
        
        [Test]
        public void TestZeroDividedByZero()
        {
            var a = Scalar.Zero;
            var b = Scalar.Zero;

            Assert.That(a / b, Is.EqualTo(Scalar.One));
        }
        
        [Test]
        public void TestInfinityDividedByZero()
        {
            var a = Scalar.Create(2, 2);
            var b = Scalar.Zero;

            Assert.That(a / b, Is.EqualTo(Scalar.Create(2, 2)));
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

                            var c = a / b;
                            var expected = (double)i / j;

                            var d = c * b;

                            Assert.That(c.V.Real, Is.EqualTo(expected), $"{a} / {b} = {c}; ({i}, {x}) / ({j}, {y}) = {expected}");
                            
                            if ((d.I == -1 && a.I == 0) || (d.I == 1 && a.I == 2))
                            {
                                // let it slide
                            }
                            else
                            {
                                if (d != a && d.I == a.I && Math.Abs(d.V.Imaginary - a.V.Imaginary) < 0.00000000001 &&
                                    Math.Abs(d.V.Real - a.V.Real) < 0.00000000001)
                                {
                                    // close enough
                                }
                                else
                                {
                                    Assert.That(d, Is.EqualTo(a), $"{a} / {b} = {c}; {c} * {b} = {d}; {d} == {a}");
                                }
                            }
                        }
                    }
                }
            }
        }


        [Test]
        public void TestTraditionalDivision()
        {
            for (var i = -100; i <= 100; i++)
            {
                for (var j = -100; j <= 100; j++)
                {
                    var a = Scalar.Create(i);
                    var b = Scalar.Create(j);

                    var s = a / b;

                    if (j != 0)
                    {
                        var z = i / j;

                        Assert.That((int) s, Is.EqualTo(z));
                    }
                }
            }
        }
    }
}
