using System.Numerics;
using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarSubtractionTests
    {
        [Test]
        public void TestLosslessInfinityZero()
        {
            var a = Scalar.Create(6, 2);
            var b = Scalar.Create(3, 0);

            // fictitious residue problem
            Assert.That(a - b, Is.EqualTo(Scalar.Create(3, 2)));
        }
        
        [Test]
        public void TestLosslessInfinityReal()
        {
            var a = Scalar.Create(6, 2);
            var b = Scalar.Create(3);

            // fictitious residue problem
            Assert.That(a - b, Is.EqualTo(Scalar.Create(3, 2)));
        }

        [Test]
        public void TestLosslessZeroInfinity()
        {
            var a = Scalar.Create(6, 0); // 6 * 0
            var b = Scalar.Create(3, 2); // 3 * inf

            Assert.That(a - b, Is.EqualTo(Scalar.Create(3, -1)));
        }

        [Test]
        public void TestLosslessNegativeOneTwo()
        {
            var a = Scalar.Create(6, -1);
            var b = Scalar.Create(3, 2);

            Assert.That(a - b, Is.EqualTo(Scalar.Create(3, -1)));
        }

        [Test]
        public void TestLosslessTwoNegativeOne()
        {
            var a = Scalar.Create(6, 2);
            var b = Scalar.Create(3, -1);

            Assert.That(a - b, Is.EqualTo(Scalar.Create(3, 2)));
        }

        [Test]
        public void TestLosslessZeroSubtraction()
        {
            var z = Scalar.Zero;
            var o = Scalar.One;

            var r = z - o;

            Assert.That(r, Is.EqualTo(Scalar.Create(-1)));
        }

        [Test]
        public void TestLossyZeroSubtraction()
        {
            var a = Scalar.Zero;
            var b = Scalar.Zero;

            Assert.That(a - b, Is.EqualTo(Scalar.Zero));
        }

        [Test]
        public void TestIntegerZeroMinusOne() 
        {
            var a = Scalar.Create(100, 0);
            var b = Scalar.One;

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1)));
        }
        
        [Test]
        public void TestLossyIntegerOneMinusZero() 
        {
            var a = Scalar.One;
            var b = Scalar.Create(100, 0);

            Assert.That(a - b, Is.EqualTo(Scalar.One));
        }

        [Test]
        public void TestTwoIntegerMinusOne() 
        {
            var a = Scalar.Create(1, 2);
            var b = Scalar.One;

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, 2)));
        }
        
        [Test]
        public void TestNegativeOneIntegerMinusOne() 
        {
            var a = Scalar.Create(1, -1);
            var b = Scalar.One;

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, -1)));
        }

        [Test]
        public void TestLosslessInfinityRealSubtraction()
        {
            var a = Scalar.One;
            var b = Scalar.Create(Complex.Infinity);

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, -1)));
        }
        
        [Test]
        public void TestLosslessInfinityInfinitySubtraction()
        {
            var a = Scalar.Create(3, 2);
            var b = Scalar.Create(5, 2);

            Assert.That(a - b, Is.EqualTo(Scalar.Create(-2)));
        }
        
        [Test]
        public void TestInfinityInfinitySubtraction()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Infinity;

            Assert.That(a - b, Is.EqualTo(Scalar.Zero));
        }

        [Test]
        public void TestInfinityInfinitySubtractionToZero()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Infinity;

            Assert.That(a - b, Is.EqualTo(Scalar.Zero));
        }
        
        [Test]
        public void TestInfinityRealSubtractionToNegativeTwo()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Create(2);
            
            // fictitious residue problem
            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, 2)));
        }
        
        [Test]
        public void TestInfinityRealSubtractionToNegativeOne()
        {
            var a = Scalar.Infinity;
            var b = Scalar.One;
            
            // fictitious residue problem
            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, 2)));
        }
        
        
        [Test]
        public void TestOneSubtractInfinityOne()
        {
            var a = Scalar.One;
            var b = Scalar.Infinity;
            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, -1)));
        }
        
        [Test]
        public void TestSubtractInfinityZero()
        {
            var a = Scalar.Infinity;
            var b = Scalar.Zero;
            Assert.That(a - b, Is.EqualTo(Scalar.Create(-1, 2)));
        }

        [Test]
        public void TestTraditionalSubtraction()
        {
            for (var i = -100; i <= 100; i++)
            {
                for (var j = -100; j <= 100; j++)
                {
                    var a = Scalar.Create(i);
                    var b = Scalar.Create(j);

                    var s = a - b;
                    var k = i - j;

                    Assert.That((int)s, Is.EqualTo(k));
                }
            }
        }
        
        [Test]
        public void TestInverseOfAddition()
        {
            for (var i1 = -100; i1 <= 100; i1++)
            {
                if (i1 == 0)
                {
                    // only applies to real-space
                    continue;
                }

                for (var i2 = -100; i2 <= 100; i2++)
                {
                    if (i2 == 0)
                    {
                        // only applies to real-space
                        continue;
                    }

                    var a = Scalar.Create(i1);
                    var b = Scalar.Create(i2);
                    var test = a - b;
                    var inv = test + b;

                    Assert.That(a.I, Is.EqualTo(inv.I), $"index of {a.ToString()} - {b.ToString()} = {test.ToString()} + {b.ToString()} = {inv.ToString()}");
                    Assert.That(a.V, Is.EqualTo(inv.V), $"value of {a.ToString()} - {b.ToString()} = {test.ToString()} + {b.ToString()} = {inv.ToString()}");
            
                }
            }
        }
    }
}
