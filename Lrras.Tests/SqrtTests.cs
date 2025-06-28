using NUnit.Framework;

namespace Lrras.Tests
{
    [TestFixture]
    public class SqrtTests
    {
        // TODO: test does not modify i


        [Test]
        public void TestSqrtOf25()
        {
            Assert.That(Scalar.Sqrt(Scalar.Create(25), 2), Is.EqualTo(Scalar.Create(5)));
        }
    }
}
