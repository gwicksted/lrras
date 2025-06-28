using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void TestLog10()
        {
            var oneHundred = Scalar.Create(100);

            var r = Scalar.Log(oneHundred, 10);

            Assert.That(r.ToString(), Is.EqualTo("(2, 1)"));
        }
        
        [Test]
        public void TestLn1()
        {
            var one = Scalar.One;

            var r = Scalar.Log(one, double.E);

            Assert.That(r.ToString(), Is.EqualTo("(1, 0)"));
        }
        
        
        [Test]
        public void TestLn0()
        {
            var zero = Scalar.Zero;

            var r = Scalar.Log(zero, double.E);

            // Assert.That(r.ToString(), Is.EqualTo("(1, -1)"));
            // without any extensions to log
            Assert.That(r.ToString(), Is.EqualTo("(1, 0)"));
        }
        
        [Test]
        public void TestLnInfinity()
        {
            var infinity = Scalar.Infinity;

            var r = Scalar.Log(infinity, double.E);

            Assert.That(r.ToString(), Is.EqualTo("(1, 1)"));
        }
    }
}
