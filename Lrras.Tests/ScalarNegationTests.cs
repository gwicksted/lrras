using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarNegationTests
    {
        [Test]
        public void TestZeroNegation()
        {
            Assert.That(-Scalar.Zero, Is.EqualTo(Scalar.Zero));
        }
        
        [Test]
        public void TestOneNegation()
        {
            Assert.That(-Scalar.One, Is.EqualTo(Scalar.Create(-1)));
        }
        
        [Test]
        public void TestInfinityNegation()
        {
            Assert.That(-Scalar.Infinity, Is.EqualTo(Scalar.Create(-1, -1)));
        }
        
        [Test]
        public void TestTraditionalNegation()
        {
            for (var i = -100; i <= 100; i++)
            {
                var a = Scalar.Create(i);
                var s = -a;
                var k = -i;

                Assert.That((int)s, Is.EqualTo(k), $"-{i}");
            }
        }
        
        [Test]
        public void TestMatchesSubtraction()
        {
            for (var i = -100; i <= 100; i++)
            {
                for (var j = -1; j <= 2; j++)
                {
                    var a = Scalar.Create(i, j);
                    var s = -a;
                    var test = Scalar.Zero - a;

                    Assert.That(s.I, Is.EqualTo(test.I));
                    Assert.That(s.V, Is.EqualTo(test.V));
                }
            }
        }
    }
}