using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class ScalarNegationTests
    {
        [Test]
        public void TestZeroNegation()
        {
            Assert.That(-Scalar.Zero, Is.EqualTo(Scalar.Create(-1, 0)));
        }
        
        [Test]
        public void TestOneNegation()
        {
            Assert.That(-Scalar.One, Is.EqualTo(Scalar.Create(-1)));
        }
        
        [Test]
        public void TestInfinityNegation()
        {
            Assert.That(-Scalar.Infinity, Is.EqualTo(Scalar.Create(-1, 2)));
        }
        
        [Test]
        public void TestTraditionalNegation()
        {
            for (var i = -100; i <= 100; i++)
            {
                var a = Scalar.Create(i);
                var s = -a;
                var k = -i;

                Assert.That((int)s, Is.EqualTo(k));
            }
        }
    }
}