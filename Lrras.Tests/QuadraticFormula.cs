using System.Numerics;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Lrras.Tests
{
    [TestFixture]
    public class QuadraticFormula
    {
        public static (Complex x1, Complex x2) SolveComplexQuadratic(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    // 0x^2 + 0x + 0 = 0
                    if (c == 0)
                    {
                        return (Complex.Zero, Complex.NaN);
                    }

                    // requires impossible numbers (outside the range of Complex, these require Scalar)
                    return (Complex.NaN, Complex.NaN);
                }

                // Linear case: bx + c = 0
                return (-c / b, Complex.NaN);
            }

            var cA = new Complex(a, 0);
            var cB = new Complex(b, 0);
            var cC = new Complex(c, 0);

            var discriminant = cB * cB - 4 * cA * cC;
            var sqrtDisc = Complex.Sqrt(discriminant);

            var x1 = (-cB + sqrtDisc) / (2 * cA);
            var x2 = (-cB - sqrtDisc) / (2 * cA);

            // returns two potential solutions because sqrt is +/-
            return (x1, x2);
        }
        
        
        
        public static Tuple<Scalar, Scalar?> SolveQuadraticImpossible(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    // 0x^2 + 0x + 0 = 0
                    if (c == 0)
                    {
                        return new Tuple<Scalar, Scalar?>(Scalar.Zero, null);
                    }

                    // 0x^2 + 0x + 10 = 0

                    var impossibleDiscriminant = new Complex(1 * 1 - 4 * 1 * c, 0);
                    
                    var ix1 = (-1 + Complex.Sqrt(impossibleDiscriminant)) / 2;
                    var ix2 = (-1 - Complex.Sqrt(impossibleDiscriminant)) / 2;

                    return new Tuple<Scalar, Scalar?>(Scalar.Create(ix1, 2), Scalar.Create(ix2, 2));
                }
                
                // Linear case: bx + c = 0
                return new Tuple<Scalar, Scalar?>(Scalar.Create(-c / b), null);
            }
            
            var discriminant = new Complex(b * b - 4 * a * c, 0);

            var x1 = Scalar.Create((-b + Complex.Sqrt(discriminant)) / (2 * a));
            var x2 = Scalar.Create((-b - Complex.Sqrt(discriminant)) / (2 * a));

            return new Tuple<Scalar, Scalar?>(x1, x2);
        }


        private void TestQuadraticSolution(double a, double b, double c, Complex x)
        {
            var test = a * Complex.Pow(x, 2) + b * x + c;
                            
            Assert.That(Math.Abs(test.Real) < 0.0000000000001 && test.Imaginary < 0.0000000000001, Is.True, $"{a}x^2 + {b}x + {c} = 0 : x = {x.ToString()}");
        }

        private void TestQuadraticSolution(double a, double b, double c, Scalar x)
        {
            var test = (a * Scalar.Pow(x, 2) + b * x + c).Evaluate();
                            
            Assert.That(Math.Abs(test.Real) < 0.0000000000001 && test.Imaginary < 0.0000000000001, Is.True, $"{a}x^2 + {b}x + {c} = 0 : x = {x.ToString()}");
        }


        [Test]
        public void SolveQuadraticInComplexSpace()
        {
            for (var a = -20; a < 20; a++)
            {
                for (var b = -20; b < 20; b++)
                {
                    for (var c = -20; c < 20; c++)
                    {
                        var traditional = SolveComplexQuadratic(a, b, c);
                        var lrras = SolveQuadraticImpossible(a, b, c);

                        if (a == 0 && b == 0)
                        {
                            if (c != 0)
                            {
                                TestQuadraticSolution(a, b, c, lrras.Item1);
                                if (lrras.Item2 != null)
                                {
                                    TestQuadraticSolution(a, b, c, lrras.Item2!.Value);
                                }
                            }
                            else // a,b,c = 0
                            {
                                Assert.That(traditional.x1, Is.EqualTo(Complex.Zero));
                                TestQuadraticSolution(a, b, c, traditional.x1);
                                TestQuadraticSolution(a, b, c, lrras.Item1);

                                Assert.That(Complex.IsNaN(traditional.x2), Is.True);
                                Assert.That(lrras.Item2, Is.Null);
                            }
                            
                        }
                        else
                        {
                            Assert.That(traditional.x1, Is.EqualTo(lrras.Item1.Evaluate()));

                            TestQuadraticSolution(a, b, c, traditional.x1);
                            TestQuadraticSolution(a, b, c, lrras.Item1);

                            if (Complex.IsNaN(traditional.x2))
                            {
                                Assert.That(lrras.Item2, Is.Null);
                            }
                            else
                            {
                                Assert.That(traditional.x2, Is.EqualTo(lrras.Item2!.Value.Evaluate()));
                                TestQuadraticSolution(a, b, c, traditional.x2);
                                TestQuadraticSolution(a, b, c, lrras.Item2!.Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
