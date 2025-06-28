using NUnit.Framework;
using System.Numerics;

namespace Lrras.Tests
{
    [TestFixture]
    public class PostProcessTests
    {
        [Test]
        public void TestThrowsWhenNaN()
        {
            Assert.Throws<ArgumentException>(() => Scalar.PostProcess(Complex.NaN, 1));
        }

        [Test]
        public void TestZeroFromZvs()
        {
            Assert.That(Scalar.PostProcess(0, 0).ToString(), Is.EqualTo("(1, 0)"));
        }

        [Test]
        public void TestZeroFromPvs()
        {
            Assert.That(Scalar.PostProcess(0, 2).ToString(), Is.EqualTo("(1, 1)"));
        }

        [Test]
        public void TestZeroFromNvs()
        {
            Assert.That(Scalar.PostProcess(0, -1).ToString(), Is.EqualTo("(1, 0)"));
        }

        [Test]
        public void TestInfinityFromRvs()
        {
            Assert.That(Scalar.PostProcess(Complex.Infinity, 1).ToString(), Is.EqualTo("(1, 2)"));
        }

        [Test]
        public void TestNegativeInfinityFromRvs()
        {
            Assert.That(Scalar.PostProcess(-Complex.Infinity, 1).ToString(), Is.EqualTo("(1, -1)"));
        }

        [Test]
        public void TestInfinityFromPvs()
        {
            Assert.That(Scalar.PostProcess(Complex.Infinity, 2).ToString(), Is.EqualTo("(1, 2)"));
        }

        [Test]
        public void TestInfinityFromNvs()
        {
            Assert.That(Scalar.PostProcess(Complex.Infinity, -1).ToString(), Is.EqualTo("(1, 0)"));
        }
        
        [Test]
        public void TestNegativeInfinityFromPvs()
        {
            Assert.That(Scalar.PostProcess(-Complex.Infinity, 2).ToString(), Is.EqualTo("(1, 1)"));
        }

        [Test]
        public void TestNegativeInfinityFromNvs()
        {
            Assert.That(Scalar.PostProcess(-Complex.Infinity, -1).ToString(), Is.EqualTo("(1, -1)"));
        }

        [Test]
        public void TestNormalCases()
        {
            Assert.That(Scalar.PostProcess(5, 1).ToString(), Is.EqualTo("(5, 1)"));
        }
    }
}
